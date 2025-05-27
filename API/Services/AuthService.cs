using System.Security.Claims;
using API.Models.Auth;
using Microsoft.AspNetCore.Authentication;

namespace API.Services;

public static class AuthService
{
    public static async Task<bool> Login(User user, HttpContext http)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.RowID)
        };

        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);
        await http.SignInAsync("Cookies", principal);
        return true;
    }
}