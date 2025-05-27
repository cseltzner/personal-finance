using API.DB.Helpers;
using API.Models.Transactions;
using Dapper;

namespace API.Data.Repositories;

public interface ITransactionAccountRepository
{
    Task<IEnumerable<TransactionAccount>> GetTransactionAccountsAsync<T>(int userId, Pagination? pagination);
    Task<TransactionAccount?> GetTransactionAccountAsync(int userId, int rowId);
    Task<int> AddTransactionAccountAsync(TransactionAccountCreateUpdateDto transactionAccount);
    Task<int> UpdateTransactionAccountAsync(int rowId, TransactionAccountCreateUpdateDto transactionAccount);
    Task<int> DeleteTransactionAccountAsync(int rowId);
}

public class TransactionAccountRepository(IDbConnectionFactory dbConnectionFactory) : ITransactionAccountRepository
{
    public async Task<IEnumerable<TransactionAccount>> GetTransactionAccountsAsync<T>(int userId,
        Pagination? pagination = null)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        pagination = pagination ?? new Pagination();

        const string sql = @"
                            select rowid, accountnumber, userid, 
                                   accountname, description, type, createddate, createdby
                            from e_account
                            where userid = @UserId
                            and voiddate is null
                            order by rowid desc
                            limit @PageSize offset @Offset
                            ";

        var parameters = new { UserId = userId, PageSize = pagination.PageSize, Offset = pagination.Offset };

        return await connection.QueryAsync<TransactionAccount>(sql, parameters);
    }

    public async Task<TransactionAccount?> GetTransactionAccountAsync(int userId, int rowId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            select rowid, accountnumber, userId, 
                                   accountname, description, type, createddate, createdby
                            from e_account
                            where userId = @UserId
                            and rowid = @RowId
                            and voiddate is null;
                            ";

        var parameters = new { UserId = userId, RowId = rowId };

        return await connection.QuerySingleOrDefaultAsync<TransactionAccount>(sql, parameters);
        ;
    }

    public async Task<int> AddTransactionAccountAsync(TransactionAccountCreateUpdateDto transactionAccount)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            insert into e_account (accountnumber, userId, accountname, description, type)
                            values (@AccountNumber, @UserId, @AccountName, @Description, @Type)
                            returning rowid;
                            ";

        var parameters = new
        {
            transactionAccount.AccountNumber,
            transactionAccount.UserId,
            transactionAccount.AccountName,
            transactionAccount.Description,
            transactionAccount.Type
        };

        var rowId = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return rowId;
    }

    public async Task<int> UpdateTransactionAccountAsync(int rowId,
        TransactionAccountCreateUpdateDto transactionAccount)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            update e_account
                            set accountnumber = coalesce(@AccountNumber, accountnumber),
                                userId = coalesce(@UserId, userId),
                                accountname = coalesce(@AccountName, accountname),
                                description = coalesce(@Description, description),
                                type = coalesce(@Type, type)
                            where rowid = @RowId
                            and voiddate is null
                            returning rowid;
                            ";

        var parameters = new
        {
            RowId = rowId,
            transactionAccount.AccountNumber,
            transactionAccount.UserId,
            transactionAccount.AccountName,
            transactionAccount.Description,
            transactionAccount.Type,
        };

        var affectedRowId = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return affectedRowId;
    }

    public async Task<int> DeleteTransactionAccountAsync(int rowId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            update e_account
                            set voiddate = now()
                            where rowid = @RowId
                            and voiddate is null
                            returning rowid;
";

        var parameters = new { RowId = rowId };

        var rowIdAffected = await connection.ExecuteScalarAsync<int>(sql, parameters);
        
        return rowIdAffected;
    }
}