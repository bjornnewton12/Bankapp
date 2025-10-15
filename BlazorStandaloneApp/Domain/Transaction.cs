namespace BlazorStandaloneApp.Domain
{
    public class Transaction : ITransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FromAccountId { get; set; } = string.Empty;
        public string ToAccountId { get; set; } = string.Empty;
        public string FromAccountName { get; set; } = string.Empty;
        public string ToAccountName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public string Currency { get; set; } = "SEK";
    }
}