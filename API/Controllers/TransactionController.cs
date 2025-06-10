using API.Data.Repositories;
using API.DB.Helpers;
using API.Models.Transactions;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public static class TransactionController
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/transactions", async (HttpContext http, [FromServices] ITransactionRepository repository,
            [FromServices] IUserRepository userRepository, [AsParameters] Pagination pagination) =>
        {
            var user = AuthService.GetCurrentUser(http, userRepository);

            if (user == null) return Results.Unauthorized();

            var transactions = await repository.GetTransactionsAsync<Transaction>(user.RowID, pagination);
            return Results.Ok(transactions);
        }).RequireAuthorization();

        app.MapGet("/api/transactions/{transactionId:int}",
            async (HttpContext http, [FromServices] ITransactionRepository repository,
                [FromServices] IUserRepository userRepository, [FromRoute] int transactionId) =>
            {
                var user = AuthService.GetCurrentUser(http, userRepository);

                if (user == null) return Results.Unauthorized();

                var transaction = await repository.GetTransactionAsync(user.RowID, transactionId);

                if (transaction == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(transaction);
            }).RequireAuthorization();

        app.MapGet("/api/transactions/multiple/{userId}",
            async (HttpContext http, [FromServices] ITransactionRepository repository,
                [FromServices] IUserRepository userRepository, string rowIds) =>
            {
                var user = AuthService.GetCurrentUser(http, userRepository);
                if (user == null) return Results.Unauthorized();

                var rowIdArr = rowIds.Split(",");

                var transactions = new List<Transaction>(); // DTO is the same as the model, so whatever
                foreach (var rowId in rowIdArr)
                {
                    if (!int.TryParse(rowId, out var parsedRowId))
                    {
                        return Results.BadRequest($"Invalid rowId: {rowId}");
                    }

                    var transaction = await repository.GetTransactionAsync(user.RowID, parsedRowId);

                    if (transaction != null)
                    {
                        transactions.Add(transaction);
                    }
                }

                return Results.Ok(transactions);
            }).RequireAuthorization();

        app.MapPost("/api/transactions", async (HttpContext http, [FromServices] ITransactionRepository repository,
            [FromServices] IUserRepository userRepository,
            [FromBody] TransactionCreateUpdateDTO transaction) =>
        {
            var user = AuthService.GetCurrentUser(http, userRepository);
            if (user == null) return Results.Unauthorized();

            // Check that the transaction has a valid userId
            if (transaction.UserID != user.RowID)
            {
                return Results.BadRequest("Transaction userId does not match the authenticated user.");
            }

            var rowId = await repository.AddTransactionAsync(transaction);
            return Results.Created($"/api/transactions/{rowId}", rowId);
        }).RequireAuthorization();

        app.MapPost("/api/transactions/multiple", async (HttpContext http,
            [FromServices] ITransactionRepository repository, [FromServices] IUserRepository userRepository,
            [FromBody] TransactionCreateUpdateDTO[] transactions) =>
        {
            var user = AuthService.GetCurrentUser(http, userRepository);
            if (user == null) return Results.Unauthorized();

            // Check that all transactions have a valid userId
            foreach (var transaction in transactions)
            {
                if (transaction.UserID != user.RowID)
                {
                    return Results.BadRequest(
                        "One or more transactions have a userId that does not match the authenticated user.");
                }
            }

            var rowIds = new List<int>();
            foreach (var transaction in transactions)
            {
                var rowId = await repository.AddTransactionAsync(transaction);
                rowIds.Add(rowId);
            }

            return Results.Created($"/api/transactions/multiple", rowIds);
        }).RequireAuthorization();

        app.MapPut("/api/transactions/{transactionId:int}",
            async (HttpContext http, [FromServices] ITransactionRepository repository,
                [FromServices] IUserRepository userRepository, [FromRoute] int transactionId,
                TransactionCreateUpdateDTO transaction) =>
            {
                var user = AuthService.GetCurrentUser(http, userRepository);
                if (user == null) return Results.Unauthorized();

                // Check that the user ID in the transaction matches the authenticated user
                if (transaction.UserID != user.RowID)
                {
                    return Results.BadRequest("Transaction userId does not match the authenticated user.");
                }

                // Check that the transaction exists and the user has permission to update it
                var existingTransaction = await repository.GetTransactionAsync(user.RowID, transactionId);
                if (existingTransaction == null)
                {
                    return Results.NotFound();
                }

                var updatedRowId = await repository.UpdateTransactionAsync(transactionId, transaction);

                if (updatedRowId == 0)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updatedRowId);
            }).RequireAuthorization();

        app.MapDelete("/api/transactions/{transactionId:int}",
            async (HttpContext http, [FromServices] ITransactionRepository repository,
                [FromServices] IUserRepository userRepository, [FromRoute] int transactionId) =>
            {
                var user = AuthService.GetCurrentUser(http, userRepository);
                if (user == null) return Results.Unauthorized();
                
                // Check that the transaction exists and the user has permission to delete it
                var existingTransaction = await repository.GetTransactionAsync(user.RowID, transactionId);
                if (existingTransaction == null)
                {
                    return Results.NotFound();
                }
                
                var deletedRowId = await repository.DeleteTransactionAsync(transactionId);

                if (deletedRowId == 0)
                {
                    return Results.NotFound();
                }

                return Results.Ok(deletedRowId);
            }).RequireAuthorization();

        /*
         // Rework this in a smarter way
        app.MapDelete("/api/transactions/multiple",
            async ([FromServices] ITransactionRepository repository, [FromQuery] string rowIds) =>
            {
                var rowIdArr = rowIds.Split(",");

                var deletedCount = 0;
                foreach (var rowId in rowIdArr)
                {
                    if (!int.TryParse(rowId, out var parsedRowId))
                    {
                        return
                            Results.BadRequest(
                                $"Invalid rowId: {rowId}"); // Could be an issue returning a 400 when other rows have already been deleted
                    }

                    var deletedRowId = await repository.DeleteTransactionAsync(parsedRowId);
                    if (deletedRowId > 0)
                    {
                        deletedCount++;
                    }
                }

                return Results.Ok(new { DeletedCount = deletedCount });
            }).RequireAuthorization();
            */
    }
}