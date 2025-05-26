using System.Data;
using Npgsql;

namespace API.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _config;

    public DbConnectionFactory(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
}
