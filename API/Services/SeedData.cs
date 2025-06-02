using Dapper;
using Npgsql;

namespace API.Services;

public static class SeedData
{
    public static async void Seed(string[] seedDataFiles, NpgsqlConnection connection)
    {
        var alreadySeeded = await connection.ExecuteScalarAsync(
            @"select SettingValue 
              from e_GlobalSettings 
              where SettingName = 'InitialDataSeededYN'"
        ) as string == "1";

        if (alreadySeeded)
        {
            Console.WriteLine("Database already seeded.");
            return;
        }

        foreach (var file in seedDataFiles)
        {
            var sql = await File.ReadAllTextAsync(file);
            if (!string.IsNullOrWhiteSpace(sql))
            {
                await connection.ExecuteAsync(sql);
                Console.WriteLine($"Seeded data from {file}");
            }
        }

        // Mark the database as seeded
        await connection.ExecuteAsync(
            @"update e_GlobalSettings 
              set SettingValue = '1' 
              where SettingName = 'InitialDataSeededYN'");
    }
}