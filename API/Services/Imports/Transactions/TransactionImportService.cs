using API.Models.Transactions;
using API.Services.Imports.Transactions.Handlers;

namespace API.Services.Imports.Transactions;

public interface ITransactionImportHandler
{
    List<Transaction> Parse(Stream fileStream);
}


public class TransactionImportService(IEnumerable<ITransactionImportHandler> handlers)
{
    private readonly Dictionary<TransactionImportFormat, ITransactionImportHandler> _handlers = new()
    {
        { TransactionImportFormat.FidelityCsv, handlers.OfType<FidelityCsvImportHandler>().First() },
        // { TransactionImportFormat.ChaseCsv, handlers.OfType<ChaseCsvImportHandler>().First() },
        // { TransactionImportFormat.DiscoverCsv, handlers.OfType<DiscoverCsvImportHandler>().First() },
    };

    public List<Transaction> Import(Stream fileStream, TransactionImportFormat importFormat)
    {
        var handler = _handlers[importFormat];
        return handler.Parse(fileStream);
    }
}
