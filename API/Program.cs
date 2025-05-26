using API.Controllers;
using API.Data;
using API.Data.Repositories;
using API.Middleware;
using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<ITransactionAccountRepository, TransactionAccountRepository>();

var app = builder.Build();

app.UseMiddleware<DbExceptionMiddleware>();

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
foreach (var file in Directory.GetFiles("DB/Init/Tables", "*.sql"))
{
    var sql = File.ReadAllText(file);
    conn.Execute(sql);
}

app.MapGet("/", () =>
    {
        return conn.QuerySingle("select 'hello' as message");
    })
    .WithName("Hello")
    .WithOpenApi();

app.MapTransactionAccountEndpoints();

app.Run();
