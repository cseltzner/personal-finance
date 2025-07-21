namespace API.Controllers;

using API.Data.Repositories;
using API.DB.Helpers;
using API.Models.Transactions;
using Microsoft.AspNetCore.Mvc;

public static class TransactionCategoryController
{
    public static void MapTransactionCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/transactioncategories", async ([FromServices] ITransactionCategoryRepository repository, [AsParameters] Pagination pagination) =>
        {
            var categories = await repository.GetTransactionCategoriesAsync(pagination);
            return Results.Ok(categories);
        });

        app.MapGet("/api/transactioncategories/{categoryId}", async ([FromServices] ITransactionCategoryRepository repository, [FromRoute] string categoryId) =>
        {
            var category = await repository.GetTransactionCategoryAsync(categoryId);
            if (category == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(category);
        });

        app.MapPost("/api/transactioncategories", async ([FromServices] ITransactionCategoryRepository repository, [FromBody] TransactionCategoryDTO transactionCategory) =>
        {
            return Results.Problem("Category creation is currently disabled.", statusCode: 403);

            await repository.AddTransactionCategoryAsync(transactionCategory);
            return Results.Created($"/api/transactioncategories/{transactionCategory.CategoryID}", transactionCategory);
        }).RequireAuthorization();

        app.MapPut("/api/transactioncategories/{categoryId}", async ([FromServices] ITransactionCategoryRepository repository, [FromRoute] string categoryId, [FromBody] TransactionCategoryDTO transactionCategory) =>
        {
            return Results.Problem("Category updates are currently disabled.", statusCode: 403);
            
            var updatedCategory = await repository.UpdateTransactionCategoryAsync(categoryId, transactionCategory);
            if (updatedCategory == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(updatedCategory);
        }).RequireAuthorization();

        app.MapDelete("/api/transactioncategories/{categoryId}", async ([FromServices] ITransactionCategoryRepository repository, [FromRoute] string categoryId) =>
        {
            return Results.Problem("Category deletion is currently disabled.", statusCode: 403);
            
            var existingCategory = await repository.GetTransactionCategoryAsync(categoryId);
            if (existingCategory == null)
            {
                return Results.NotFound();
            }

            await repository.DeleteTransactionCategoryAsync(categoryId);
            return Results.NoContent();
        }).RequireAuthorization();
    }
}