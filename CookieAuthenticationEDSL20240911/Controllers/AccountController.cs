using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuthenticationEDSL20240911.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(string login, string password)
    {
        if (login != "admin" || password != "admin123") return Unauthorized("Credenciales incorrectas");

        var claims = new[] {
            new Claim(ClaimTypes.Name, login)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = false
        };

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        return Ok("Inicio de sesión exitoso");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok("Cerró sesión correctamente");
    }
}
