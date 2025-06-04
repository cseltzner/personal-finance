using API.Data.Repositories;
using API.DB.Helpers;
using API.Models.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public static class TransactionController
{
    public static void MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/transactions", async ([FromServices] ITransactionRepository repository,
            [FromQuery] int userId, [AsParameters] Pagination pagination) =>
        {
            var transactions = await repository.GetTransactionsAsync<Transaction>(userId, pagination);
            return Results.Ok(transactions);
        });

        app.MapGet("/api/transactions/{transactionId:int}",
            async ([FromServices] ITransactionRepository repository, [FromRoute] int transactionId) =>
            {
                // Need some user security checks here
                var transaction = await repository.GetTransactionAsync(transactionId);

                if (transaction == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(transaction);
            });

        app.MapGet("/api/transactions/multiple/{userId}",
            async ([FromServices] ITransactionRepository repository, string rowIds) =>
            {
                var rowIdArr = rowIds.Split(",");

                var transactions = new List<Transaction>(); // DTO is the same as the model, so whatever
                foreach (var rowId in rowIdArr)
                {
                    if (!int.TryParse(rowId, out var parsedRowId))
                    {
                        return Results.BadRequest($"Invalid rowId: {rowId}");
                    }

                    var transaction = await repository.GetTransactionAsync(parsedRowId);

                    if (transaction != null)
                    {
                        transactions.Add(transaction);
                    }
                }

                return Results.Ok(transactions);
            });

        app.MapPost("/api/transactions", async ([FromServices] ITransactionRepository repository,
            [FromBody] TransactionCreateUpdateDTO transaction) =>
        {
            var rowId = await repository.AddTransactionAsync(transaction);
            return Results.Created($"/api/transactions/{rowId}", rowId);
        });

        app.MapPost("/api/transactions/multiple", async ([FromServices] ITransactionRepository repository,
            [FromBody] TransactionCreateUpdateDTO[] transactions) =>
        {
            var rowIds = new List<int>();
            foreach (var transaction in transactions)
            {
                var rowId = await repository.AddTransactionAsync(transaction);
                rowIds.Add(rowId);
            }

            return Results.Created($"/api/transactions/multiple", rowIds);
        });

        app.MapPut("/api/transactions/{transactionId:int}",
            async ([FromServices] ITransactionRepository repository, [FromRoute] int transactionId,
                TransactionCreateUpdateDTO transaction) =>
            {
                var updatedRowId = await repository.UpdateTransactionAsync(transactionId, transaction);

                if (updatedRowId == 0)
                {
                    return Results.NotFound();
                }

                return Results.Ok(updatedRowId);
            });

        app.MapDelete("/api/transactions/{transactionId:int}",
            async ([FromServices] ITransactionRepository repository, [FromRoute] int transactionId) =>
            {
                var deletedRowId = await repository.DeleteTransactionAsync(transactionId);

                if (deletedRowId == 0)
                {
                    return Results.NotFound();
                }

                return Results.Ok(deletedRowId);
            });

        app.MapDelete("/api/transactions/multiple",
            async ([FromServices] ITransactionRepository repository, [FromQuery] string rowIds) =>
            {
                var rowIdArr = rowIds.Split(",");

                var deletedCount = 0;
                foreach (var rowId in rowIdArr)
                {
                    if (!int.TryParse(rowId, out var parsedRowId))
                    {
                        return Results.BadRequest($"Invalid rowId: {rowId}"); // Could be an issue returning a 400 when other rows have already been deleted
                    }

                    var deletedRowId = await repository.DeleteTransactionAsync(parsedRowId);
                    if (deletedRowId > 0)
                    {
                        deletedCount++;
                    }
                }

                return Results.Ok(new { DeletedCount = deletedCount });
            });
    }
}