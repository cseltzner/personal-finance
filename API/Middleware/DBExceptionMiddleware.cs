using API.DB.Helpers;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace API.Middleware;

public class DbExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public DbExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (PostgresException ex) when (ex.SqlState == DBErrorCode.ForeignKeyViolation)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Foreign key constraint failed.");
        }
        catch (PostgresException ex) when (ex.SqlState == DBErrorCode.PrimaryKeyViolation)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync("Duplicate primary key or unique constraint failed.");
        }
    }
}