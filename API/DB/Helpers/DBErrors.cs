namespace API.DB.Helpers;

public static class DBErrorCode
{
    // PostgreSQL Error Codes
    public static string PrimaryKeyViolation = "23505";
    public static string ForeignKeyViolation = "23503";
    public static string NotNullViolation = "23502";
    public static string CheckViolation = "23514";
}