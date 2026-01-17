using FluentValidation;
using KanbanBackend.Application;
using KanbanBackend.Application.Boards.Commands.CreateBoard;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Application.Common.Mappings;
using KanbanBackend.Infrastructure.Persistance;
using KanbanBackend.Infrastructure.Persistance.Repositories;
using KanbanBackend.Infrastructure.Services.Authorization;
using KanbanBackend.Infrastructure.Services.PassHasher;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

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
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateBoardCommand).Assembly)
                );
            builder.Services.AddValidatorsFromAssembly(typeof(CreateBoardCommandValidator).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            builder.Services.AddScoped<IBoardRepository, BoardRepository>();
            builder.Services.AddScoped<IColumnRepository, ColumnRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IUserRepository, IUserRepository>();

            builder.Services.AddDbContext<KanbanDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
