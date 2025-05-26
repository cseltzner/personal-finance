namespace API.Models.Transactions;

public class TransactionAccount
{
    public int RowID { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public string? AccountName { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; } = string.Empty; // e.g. Checking, Savings, Credit Card, etc.
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
}

// Read DTO
public class TransactionAccountDto
{
    public int RowID { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public string? AccountName { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; } = string.Empty; // e.g. Checking, Savings, Credit Card, etc.
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
}

public class TransactionAccountCreateUpdateDto
{
    public string AccountNumber { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public string? AccountName { get; set; }
    public string? Description { get; set; }
    public string Type { get; set; } = string.Empty; // e.g. Checking, Savings, Credit Card, etc.
}