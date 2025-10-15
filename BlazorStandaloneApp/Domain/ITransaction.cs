namespace BlazorStandaloneApp.Domain
{
    public interface ITransaction
    {
        Guid Id { get; }
        string FromAccountId { get; }
        string ToAccountId { get; }
        string FromAccountName { get; }
        string ToAccountName { get; }
        decimal Amount { get; }
        DateTime LastUpdated { get; }
        string Currency { get; }
    }
}