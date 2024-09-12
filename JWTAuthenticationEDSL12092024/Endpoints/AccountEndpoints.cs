using JWTAuthenticationEDSL12092024.Auth;

namespace JWTAuthenticationEDSL12092024.Endpoints;

public static class AccountEndpoints
{
    public static void AddAccountEndpoints(this WebApplication app)
    {
        app.MapPost("/account/login", (string login, string password, IJWTAuthenticationService autheService) =>
        {
            if (login != "admin" || password != "admin123") return Results.Unauthorized();

            var token = autheService.Authenticate(login);

            return Results.Ok(token);
        });
    }
}
