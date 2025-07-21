// insert into l_TransactionCategory (CategoryID, ParentCategory, Category, ShortName, CreatedBy) values ('0742', 'Agricultural Services', 'Veterinary Services', 'Veterinary Services', 'Chase');

namespace API.Models.Transactions;

public class TransactionCategory
{
    public string CategoryID { get; set; } = string.Empty;
    public string ParentCategory { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }

    public override string ToString() => Category;
}

public class TransactionCategoryDTO
{
    public string CategoryID { get; set; } = string.Empty;
    public string ParentCategory { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;

    public override string ToString() => Category;
}