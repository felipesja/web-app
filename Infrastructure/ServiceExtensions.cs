﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using webapp.Data;
using webapp.Services;

namespace webapp.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração de Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new List<string>()
                }
            });
        });

        // Serviços do banco de dados
        services.AddDbContext<WebAppDbContext>(db =>
            db.UseSqlServer(configuration.GetConnectionString("WebAppConnectionString")),
            ServiceLifetime.Singleton);

        // Serviços do Identity
        services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<WebAppDbContext>();

        // Serviços customizados
        services.AddSingleton<IProductService, ProductService>();

        // Autorizações
        services.AddAuthorization();

        return services;
    }
}