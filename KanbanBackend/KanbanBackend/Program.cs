using FluentValidation;
using KanbanBackend.Application;
using KanbanBackend.Application.Boards.Commands.CreateBoard;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Application.Common.Mappings;
using KanbanBackend.Infrastructure.Persistance;
using KanbanBackend.Infrastructure.Persistance.Repositories;
using KanbanBackend.Infrastructure.Services.Authorization;
using KanbanBackend.Infrastructure.Services.HandleRecursiveDelete;
using KanbanBackend.Infrastructure.Services.PassHasher;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

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
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddSingleton<IHandleRecursiveDeleteService, HandleRecursiveDeleteService>();
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
            });

            builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            builder.Services.AddScoped<IBoardRepository, BoardRepository>();
            builder.Services.AddScoped<IColumnRepository, ColumnRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddDbContext<KanbanDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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

            app.Run();
        }
    }
}
