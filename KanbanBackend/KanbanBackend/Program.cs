using FluentValidation;
using KanbanBackend.Application;
using KanbanBackend.Application.Boards.Commands.CreateBoard;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Application.Common.Mappings;
using KanbanBackend.Infrastructure.Persistance;
using KanbanBackend.Infrastructure.Persistance.Repositories;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using KanbanBackend.Infrastructure.Services.Authorization;
using KanbanBackend.Infrastructure.Services.HandleRecursiveDelete;
using KanbanBackend.Infrastructure.Services.PassHasher;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace KanbanBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(cfg =>
                cfg.AddProfile<MappingProfile>()
            );

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            var jwtSection = builder.Configuration.GetSection("Jwt");
            var jwtSettings = jwtSection.Get<JwtSettings>();

            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });


            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateBoardCommand).Assembly)
                );
            builder.Services.AddValidatorsFromAssembly(typeof(CreateBoardCommandValidator).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Controllers + Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kanban API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {token}'"
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string [] {}
                    }
                });
            });

            builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            builder.Services.AddScoped<IBoardRepository, BoardRepository>();
            builder.Services.AddScoped<IColumnRepository, ColumnRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IHandleRecursiveDeleteService, HandleRecursiveDeleteService>();
            builder.Services.AddScoped<IActivityLoggerService, ActivityLoggerService>();

            builder.Services.AddDbContext<KanbanDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });


            var app = builder.Build();

            // Enable Swagger UI and redirect root to Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kanban API v1");
            });

            // Redirect root URL to Swagger UI
            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(myAllowSpecificOrigins);

            app.Run();
        }
    }
}
