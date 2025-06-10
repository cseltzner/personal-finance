using System.Security.Claims;
using API.Data.Repositories;
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
            new Claim(ClaimTypes.NameIdentifier, user.RowID.ToString())
        };

        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);
        await http.SignInAsync("Cookies", principal);
        return true;
    }

    public static User? GetCurrentUser(HttpContext http, IUserRepository userRepository)
    {
        var user = http.User;
        if (user.Identity?.IsAuthenticated != true)
            return null;

        var username = user.Identity.Name;
        if (string.IsNullOrWhiteSpace(username))
            return null;
        
        return userRepository.GetUserByUsernameAsync(username).Result;
    }
}