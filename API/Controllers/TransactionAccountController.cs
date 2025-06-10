using API.Data.Repositories;
using API.DB.Helpers;
using API.Models.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public static class TransactionAccountController
{
    public static void MapTransactionAccountEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/transactionaccounts", async ([FromServices] ITransactionAccountRepository repository,
            [FromQuery] int userId, [AsParameters] Pagination pagination) =>
        {
            var accounts = await repository.GetTransactionAccountsAsync<TransactionAccountDto>(userId, pagination);
            return Results.Ok(accounts);
        }).RequireAuthorization();
        
        app.MapGet("/api/transactionaccounts/{userId}/{rowId:int}", async ([FromServices] ITransactionAccountRepository repository, [FromRoute] int userId, [FromRoute] int rowId) =>
        {
            var account = await repository.GetTransactionAccountAsync(userId, rowId);

            if (account == null)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(account);
        }).RequireAuthorization();
        
        app.MapGet("/api/transactionaccounts/multiple/{userId}", async ([FromServices] ITransactionAccountRepository repository, [FromRoute] int userId, [FromQuery] string rowIds) =>
        {
            var rowIdArr = rowIds.Split(",");
            
            var accounts = new List<TransactionAccount>(); // DTO is the same as the model, so whatever
            foreach (var rowId in rowIdArr)
            {
                if (!int.TryParse(rowId, out var parsedRowId))
                {
                    return Results.BadRequest($"Invalid rowId: {rowId}");
                }
                
                var account = await repository.GetTransactionAccountAsync(userId, parsedRowId);
                if (account != null)
                {
                    accounts.Add(account);
                }
            }
            return Results.Ok(accounts);
        }).RequireAuthorization();

        app.MapPost("/api/transactionaccounts", async ([FromServices] ITransactionAccountRepository repository, [FromBody] TransactionAccountCreateUpdateDto transactionAccount) =>
        {
            var rowId = await repository.AddTransactionAccountAsync(transactionAccount);
            return Results.Created($"/api/transactionaccounts/{rowId}", rowId);
        }).RequireAuthorization();
        
        app.MapPost("/api/transactionaccounts/multiple", async ([FromServices] ITransactionAccountRepository repository, [FromBody] TransactionAccountCreateUpdateDto[] transactionAccounts) =>
        {
            var rowIds = new List<int>();
            foreach (var account in transactionAccounts)
            {
                var rowId = await repository.AddTransactionAccountAsync(account);
                rowIds.Add(rowId);
            }
            return Results.Created($"/api/transactionaccounts/multiple", rowIds);
        }).RequireAuthorization();

        app.MapPut("/api/transactionaccounts/{rowId:int}", async ([FromServices] ITransactionAccountRepository repository, [FromRoute] int rowId,
            TransactionAccountCreateUpdateDto transactionAccount) =>
        {
            var updatedRowId = await repository.UpdateTransactionAccountAsync(rowId, transactionAccount);
            
            if (updatedRowId == 0)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(updatedRowId);
        }).RequireAuthorization();

        app.MapDelete("/api/transactionaccounts/{rowId:int}", async ([FromServices] ITransactionAccountRepository repository, [FromRoute] int rowId) =>
        {
            var deletedRowId = await repository.DeleteTransactionAccountAsync(rowId);
            
            if (deletedRowId == 0)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(deletedRowId);
        }).RequireAuthorization();
        
        app.MapDelete("/api/transactionaccounts/multiple", async ([FromServices] ITransactionAccountRepository repository, [FromQuery] string rowIds) =>
        {
            var rowIdArr = rowIds.Split(",");
            
            var deletedCount = 0;
            foreach (var rowId in rowIdArr)
            {
                if (!int.TryParse(rowId, out var parsedRowId))
                {
                    return Results.BadRequest($"Invalid rowId: {rowId}");
                }
                
                var isDeleted = await repository.DeleteTransactionAccountAsync(parsedRowId);
                
                if (isDeleted > 0)
                {
                    deletedCount++;
                }
            }

            if (deletedCount == 0)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(deletedCount);
        }).RequireAuthorization();
    }
}