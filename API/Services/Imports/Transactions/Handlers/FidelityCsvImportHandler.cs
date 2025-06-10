using System.Globalization;
using API.Models.Transactions;
using CsvHelper.Configuration;

namespace API.Services.Imports.Transactions.Handlers;

public class FidelityCsvImportHandler: ITransactionImportHandler
{
    public List<Transaction> Parse(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvHelper.CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
        });
        
        var transactions = new List<Transaction>();

        foreach (var record in csv.GetRecords<dynamic>())
        {
            transactions.Add(new Transaction
            {
                Date = DateTime.Parse(record["Date"]),
                Type = record["Transaction"], // Debit or Credit
                Origin = "Fidelity",
                Source = "Import",
                Description = record["Name"],
                Note = record["Memo"],
                Amount = decimal.Parse(record["Amount"], CultureInfo.InvariantCulture),
            });
        }

        return transactions;
    }
}