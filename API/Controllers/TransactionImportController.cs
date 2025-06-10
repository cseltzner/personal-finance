using API.Data.Repositories;
using API.Services;
using API.Services.Imports.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public static class TransactionImportController
{
    public static void MapTransactionImportEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/transactionimport",
            async (HttpRequest request, TransactionImportFormat format, TransactionImportService importer,
                [FromServices] ITransactionRepository transactionRepository) =>
            {
                var importFile = request.Form.Files["importFile"];
                
                if (importFile == null || importFile.Length == 0)
                {
                    return Results.BadRequest("No file uploaded.");
                }
                
                if (!Enum.IsDefined(typeof(TransactionImportFormat), format))
                {
                    return Results.BadRequest("Invalid import format specified.");
                }
                
                // Validate file type (excel or csv)
                var allowedExtensions = new[] { ".csv", ".xlsx" };
                var fileExtension = Path.GetExtension(importFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Results.BadRequest("Invalid file type. Only CSV and Excel files are allowed.");
                }
                
                // Validate file size (e.g., max 10MB)
                const long maxFileSize = 10 * 1024 * 1024; // 10 MB
                if (importFile.Length > maxFileSize)
                {
                    return Results.BadRequest("File size exceeds the maximum limit of 10 MB.");
                }

                await using var stream = importFile.OpenReadStream();
                var transactions = importer.Import(stream, format);

                // Insert transactions into DB here...
                foreach (var transaction in transactions)
                {
                    await transactionRepository.AddTransactionAsync(transaction.ToCreateUpdateDTO());
                }
                
                return Results.Ok(transactions.Count);
            });
    }
}