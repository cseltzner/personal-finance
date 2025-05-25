using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

using var conn = new NpgsqlConnection(connStr);
conn.Open();

// Load .sql files and execute on startup
foreach (var file in Directory.GetFiles("init-sql", "*.sql"))
{
    var sql = File.ReadAllText(file);
    conn.Execute(sql);
}

app.MapGet("/", () => "Hello from Minimal API")
    .WithName("Hello")
    .WithOpenApi();

app.Run();
