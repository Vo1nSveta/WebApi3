using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WorkshopWebApi.Data;
using WorkshopWebApi.Models;

namespace WorkshopWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MyAppContext>();

            // Add services to the container.
            builder.Services.AddDbContext<MyAppContext>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Описываем схему безопасности (JWT Bearer)
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {                    
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Введите ваш JWT токен в поле ниже."
                });

                // Добавляем глобальное требование безопасности для всех эндпоинтов
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
           Array.Empty<string>()
                }
            });
        });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // требуется валидация издателя
                        ValidIssuer = JwtAuthOptions.Issuer, // издатель
                        ValidateAudience = true, // требуется валидация потребителя
                        ValidAudience = JwtAuthOptions.Audience, // потребитель
                        ValidateLifetime = true, // требуется валидация времени жизни
                        IssuerSigningKey = JwtAuthOptions.SecurityKey, // ключ безопасности
                        ValidateIssuerSigningKey = true, // валидация ключа безопасности
                    };
                });
            builder.Services.AddAuthentication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
