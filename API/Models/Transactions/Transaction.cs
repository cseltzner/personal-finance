namespace API.Models.Transactions;

public class Transaction
{
    public int RowID { get; set; }
    public int UserID { get; set; }
    public string TransactionID { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AccountID { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }

    public TransactionCreateUpdateDTO ToCreateUpdateDTO()
    {
        return new TransactionCreateUpdateDTO
        {
            UserID = UserID,
            TransactionID = TransactionID,
            Date = Date,
            Type = Type,
            Origin = Origin,
            Description = Description,
            AccountID = AccountID,
            Category = Category,
            Amount = Amount,
            Note = Note,
            Source = Source
        };
    }
}

public class TransactionCreateUpdateDTO
{
    public int UserID { get; set; }
    public string TransactionID { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AccountID { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
}