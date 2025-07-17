using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyBlockForumServer.Auth;
using System.Text;

namespace MyBlockForumServer.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtOptions? jwtOptions = configuration.GetSection("JWTOptions").Get<JwtOptions>();
            ArgumentNullException.ThrowIfNull(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["snezhok_cookie"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
