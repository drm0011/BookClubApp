using BookClubApp.DAL;
using BookClubApp.DAL.Models;
using BookClubApp.Core.Interfaces;
using BookClubApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookClubApp.DAL.Repos;

namespace BookClubApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSignalR();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BookClubAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IReadingListRepository, ReadingListRepository>();
            builder.Services.AddScoped<IReadingListService, ReadingListService>();

            builder.Services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();

            builder.Services.AddHttpClient<IOpenLibraryService, OpenLibraryService>();

            builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            builder.Services.AddScoped<IChatMessageService, ChatMessageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers(); 
            app.MapHub<ChatHub>("/chatHub");  // Map SignalR hub

            app.Run();
        }
    }
}
