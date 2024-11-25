using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Web.Model;

namespace WebApp.Web.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager, [FromBody] object empty) =>
            {
                if (empty != null)
                {
                    await signInManager.SignOutAsync();
                    return Results.Ok();
                }
                return Results.Unauthorized();
            })
            .WithOpenApi()
            .RequireAuthorization();

            app.MapPost("/forgotPasswordCustom", async (ForgotPasswordModel model, UserManager<IdentityUser> userManager) =>
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return Results.BadRequest("Usuário não encontrado.");

                // Gerar o token de redefinição de senha
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                return Results.Ok(token);
            })
            .WithOpenApi();

            app.MapPost("/resetPasswordCustom", async (ResetPasswordModel model, UserManager<IdentityUser> userManager) =>
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return Results.BadRequest("Usuário não encontrado.");

                // Verificar o token de redefinição de senha
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (!result.Succeeded)
                    return Results.BadRequest("Erro ao redefinir a senha.");

                return Results.Ok("Senha redefinida com sucesso.");
            })
            .WithOpenApi();
        }
    }
}
