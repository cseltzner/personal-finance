using API.Data.Repositories;
using API.Models.Auth;
using API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public static class AuthController
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginDto req, HttpContext http, IUserRepository repo) =>
        {
            var user = await repo.GetUserByUsernameAsync(req.Username);
            if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Results.Unauthorized();

            await AuthService.Login(user, http);

            return Results.Ok();
        });


        app.MapPost("/api/auth/register", async ([FromBody] RegisterDto req, [FromServices] IUserRepository repo, HttpContext http) =>
        {
            if (string.IsNullOrWhiteSpace(req.Username) ||
                string.IsNullOrWhiteSpace(req.Email) ||
                string.IsNullOrWhiteSpace(req.Password))
            {
                return Results.BadRequest("All fields are required.");
            }

            // Check if user already exists
            var existingUserField = await repo.UserNameOrEmailExistsAsync(req.Username, req.Email);

            if (existingUserField == "Username")
                return Results.BadRequest("Username is already taken.");
            if (existingUserField == "Email")
                return Results.BadRequest("Email is already in use.");

            // Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

            // Create user
            var user = new RegisterUser
            {
                Username = req.Username,
                Email = req.Email,
                PasswordHash = passwordHash,
                FirstName = req.FirstName,
                LastName = req.LastName,
            };

            var userId = await repo.CreateUserAsync(user);

            AuthService.Login(new User()
            {
                RowID = userId.ToString(),
                Username = user.Username,
            }, http);
            
            return Results.Created($"/api/auth/register/{userId}", userId);
        });
        
        app.MapGet("/api/auth/logout", async (HttpContext http) =>
        {
            await http.SignOutAsync("Cookies");
            return Results.Ok();
        });
    }
}