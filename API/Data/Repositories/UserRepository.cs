using API.DB.Helpers;
using API.Models.Auth;
using Dapper;

namespace API.Data.Repositories;

public interface IUserRepository
{
    Task<string> UserNameOrEmailExistsAsync(string userName, string email);
    Task<int> CreateUserAsync(RegisterUser user);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
}

public class UserRepository(IDbConnectionFactory dbConnectionFactory) : IUserRepository
{
    public async Task<string> UserNameOrEmailExistsAsync(string userName, string email)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
            select username, email
            from e_User
            where username = @UserName 
               or email = @Email
            limit 1;
        ";


        var parameters = new { UserName = userName, Email = email };

        var result = await connection.QuerySingleOrDefaultAsync<(string Username, string Email)>(sql, parameters);

        if (result.Username == userName)
            return "Username";
        if (result.Email == email)
            return "Email";
        return "";
    }

    public async Task<int> CreateUserAsync(RegisterUser user)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
            insert into e_User (username, passwordhash, email, firstname, lastname, phone)
            values (@Username, @PasswordHash, @Email, @FirstName, @LastName, @PhoneNumber)
            returning rowid;
            ";
        
        var parameters = new
        {
            user.Username,
            user.PasswordHash,
            user.Email,
            user.FirstName,
            user.LastName,
            user.PhoneNumber
        };
        
        return await connection.ExecuteScalarAsync<int>(sql, parameters);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
            select rowid, username, email, passwordhash, firstname, lastname, phone, userrole, createddate, createdby
            from e_User
            where username = @Username
            limit 1;
        ";

        var parameters = new { Username = username };

        return await connection.QuerySingleOrDefaultAsync<User>(sql, parameters);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string sql = @"
            select rowid, username, email, passwordhash, firstname, lastname, phone, userrole, createddate, createdby
            from e_User
            where email = @Email
            limit 1;
        ";

        var parameters = new { Email = email };

        return await connection.QuerySingleOrDefaultAsync<User>(sql, parameters);
    }
}