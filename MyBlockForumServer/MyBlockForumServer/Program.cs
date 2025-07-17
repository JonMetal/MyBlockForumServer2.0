using MyBlockForumServer.Auth;
using MyBlockForumServer.Database.Entities;
using MyBlockForumServer.Database.Repositories;
using MyBlockForumServer.Database.Services;
using MyBlockForumServer.Extensions;
using Thread = MyBlockForumServer.Database.Entities.Thread;

namespace MyBlockForumServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IRepository<Thread>, ThreadRepository>();
            builder.Services.AddScoped<IRepository<ThreadTheme>, ThreadThemeRepository>();
            builder.Services.AddScoped<IThreadService, ThreadService>();

            builder.Services.AddScoped<IRepository<Message>, MessageRepository>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Status>, StatusRepository>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApiAAuthentication(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
