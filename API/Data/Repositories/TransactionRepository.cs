using Dapper;

namespace API.Data.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<T>> GetTransactionsAsync<T>(object? parameters = null);}

public class TransactionRepository : ITransactionRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    
    public TransactionRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task<IEnumerable<T>> GetTransactionsAsync<T>(object? parameters = null)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        
        const string sql = "SELECT * FROM l_transactioncategory"; // Adjust the SQL query as needed
        
        return await connection.QueryAsync<T>(sql, parameters);
    }
}