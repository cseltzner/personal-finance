using API.Controllers;
using API.Data;
using API.Data.Repositories;
using API.Middleware;
using API.Services;
using API.Services.Imports.Transactions;
using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<ITransactionAccountRepository, TransactionAccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
builder.Services.AddScoped<TransactionImportService>();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.ExpireTimeSpan = TimeSpan.FromHours(6);
        options.SlidingExpiration = true;
        // Send 401 Unauthorized instead of redirecting to login page
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevCors", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("LocalDevCors");

app.UseMiddleware<DbExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseHttpsRedirection();

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

using var conn = new NpgsqlConnection(connStr);
conn.Open();

// Load .sql files and execute on startup
var initSqlFiles = new[]
{
    "DB/Init/Tables/Users.sql",
    "DB/Init/Tables/Transactions.sql",
    "DB/Init/Tables/GlobalSettings.sql",
};

foreach (var file in initSqlFiles)
{
    var sql = File.ReadAllText(file);
    conn.Execute(sql);
}

var seedDataFiles = new[]
{
    "DB/Init/Seed/TransactionCategories.sql",
};

SeedData.Seed(seedDataFiles, conn);

app.MapGet("/", () =>
    {
        return conn.QuerySingle("select 'hello' as message");
    })
    .WithName("Hello")
    .WithOpenApi();

app.MapTransactionAccountEndpoints();
app.MapAuthEndpoints();
app.MapTransactionEndpoints();
app.MapTransactionImportEndpoints();
app.MapTransactionCategoryEndpoints();

app.Run();
