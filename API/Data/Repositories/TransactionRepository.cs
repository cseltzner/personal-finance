using API.DB.Helpers;
using API.Models.Transactions;
using Dapper;

namespace API.Data.Repositories;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetTransactionsAsync<T>(int userId, Pagination pagination);
    Task<Transaction> GetTransactionAsync(int transactionId);
    Task<int> AddTransactionAsync(TransactionCreateUpdateDTO transaction);
    Task<int> UpdateTransactionAsync(int transactionId, TransactionCreateUpdateDTO transaction);
    Task<int> DeleteTransactionAsync(int transactionId);
}

public class TransactionRepository(IDbConnectionFactory dbConnectionFactory) : ITransactionRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    
    public async Task<IEnumerable<Transaction>> GetTransactionsAsync<T>(int userId, Pagination pagination)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        pagination = pagination ?? new Pagination();

        const string sql = @"
            select rowid, userid, transactionid, date, type, origin, description, accountid, category, amount, note, source, createddate, createdby
            from t_transaction
            where userid = @UserId
            order by rowid desc
            limit @PageSize offset @Offset;
        ";

        var parameters = new { UserId = userId, PageSize = pagination.PageSize, Offset = pagination.Offset };

        return await connection.QueryAsync<Transaction>(sql, parameters);
    }

    public async Task<Transaction> GetTransactionAsync(int RowID)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = @"
            select rowid, userid, transactionid, date, type, origin, description, accountid, category, amount, note, source, createddate, createdby
            from t_transaction
            where RowID = @RowID;
        ";

        var parameters = new { RowID = RowID };

        return await connection.QuerySingleOrDefaultAsync<Transaction>(sql, parameters);
    }

    public async Task<int> AddTransactionAsync(TransactionCreateUpdateDTO transaction)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = @"
            insert into t_transaction (userid, transactionid, date, type, origin, description, accountid, category, amount, note, source)
            values (@UserId, @TransactionId, @Date, @Type, @Origin, @Description, @AccountId, @Category, @Amount, @Note, @Source)
            returning rowid;
        ";

        var parameters = new
        {
            UserId = transaction.UserID,
            TransactionId = transaction.TransactionID,
            Date = transaction.Date,
            Type = transaction.Type,
            Origin = transaction.Origin,
            Description = transaction.Description,
            AccountId = transaction.AccountID,
            Category = transaction.Category,
            Amount = transaction.Amount,
            Note = transaction.Note,
            Source = transaction.Source,
        };

        return await connection.ExecuteScalarAsync<int>(sql, parameters);
    }

    public async Task<int> UpdateTransactionAsync(int rowID, TransactionCreateUpdateDTO transaction)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = @"
            update t_transaction
            set transactionid = @TransactionId,
                date = @Date,
                type = @Type,
                origin = @Origin,
                description = @Description,
                accountid = @AccountId,
                category = @Category,
                amount = @Amount,
                note = @Note,
                source = @Source
            where rowid = @RowID;
        ";

        var parameters = new
        {
            RowID = rowID,
            TransactionId = transaction.TransactionID,
            Date = transaction.Date,
            Type = transaction.Type,
            Origin = transaction.Origin,
            Description = transaction.Description,
            AccountId = transaction.AccountID,
            Category = transaction.Category,
            Amount = transaction.Amount,
            Note = transaction.Note,
            Source = transaction.Source
        };

        return await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<int> DeleteTransactionAsync(int rowID)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        const string sql = @"
            delete from t_transaction
            where rowid = @RowID;
        ";

        var parameters = new { RowID = rowID };

        return await connection.ExecuteAsync(sql, parameters);
    }
}