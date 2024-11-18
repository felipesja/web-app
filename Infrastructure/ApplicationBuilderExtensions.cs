using Microsoft.AspNetCore.Identity;
using webapp.Endpoints;

namespace webapp.Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        // Ambiente de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Rotas e endpoints
        app.MapIdentityApi<IdentityUser>();
        app.MapProductEndpoints();
        app.MapAuthEndpoints();
    }
}
