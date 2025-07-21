using API.DB.Helpers;
using API.Models.Transactions;
using Dapper;

namespace API.Data.Repositories;

public interface ITransactionCategoryRepository
{
    Task<IEnumerable<TransactionCategoryDTO>> GetTransactionCategoriesAsync(Pagination? pagination);
    Task<TransactionCategoryDTO?> GetTransactionCategoryAsync(string categoryId);
    Task<int> AddTransactionCategoryAsync(TransactionCategoryDTO transactionCategory);
    Task<int> UpdateTransactionCategoryAsync(string categoryId, TransactionCategoryDTO transactionCategory);
    Task DeleteTransactionCategoryAsync(string categoryId);
}

public class TransactionCategoryRepository(IDbConnectionFactory dbConnectionFactory) : ITransactionCategoryRepository
{
    public async Task<IEnumerable<TransactionCategoryDTO>> GetTransactionCategoriesAsync(Pagination? pagination = null)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        pagination ??= new Pagination();

        var sql = $@"
            select CategoryID, ParentCategory, Category, ShortName
            from l_TransactionCategory
            order by categoryID {pagination.Sort}
            limit @PageSize offset @Offset;
        ";

        var parameters = new { PageSize = pagination.PageSize, Offset = pagination.Offset };

        return await connection.QueryAsync<TransactionCategoryDTO>(sql, parameters);
    }

    public async Task<TransactionCategoryDTO?> GetTransactionCategoryAsync(string categoryId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            select CategoryID, ParentCategory, Category, ShortName
                            from l_TransactionCategory
                            order by categoryID asc
                            where CategoryID = @CategoryID;
                            ";

        var parameters = new { CategoryID = categoryId };

        return await connection.QuerySingleOrDefaultAsync<TransactionCategoryDTO?>(sql, parameters);
        
    }

    public async Task<int> AddTransactionCategoryAsync(TransactionCategoryDTO transactionCategory)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            insert into l_TransactionCategory (CategoryID, ParentCategory, Category, ShortName)
                            values (@CategoryID, @ParentCategory, @Category, @ShortName);
                            ";


        var rowID = await connection.ExecuteScalarAsync<int>(sql, transactionCategory);

        return rowID;
    }

    public async Task<int> UpdateTransactionCategoryAsync(string categoryId, TransactionCategoryDTO transactionCategory)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            update l_TransactionCategory
                            set ParentCategory = @ParentCategory,
                                Category = @Category,
                                ShortName = @ShortName
                            where CategoryID = @CategoryID;
                            returning rowid;
                            ";

        var parameters = new
        {
            CategoryID = categoryId,
            transactionCategory.ParentCategory,
            transactionCategory.Category,
            transactionCategory.ShortName
        };

        var rowID = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return rowID;
    }

    public async Task DeleteTransactionCategoryAsync(string categoryId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
                            delete from l_TransactionCategory
                            where CategoryID = @CategoryID;

                            ";

        var parameters = new { CategoryID = categoryId };

        await connection.ExecuteAsync(sql, parameters);
    }
}