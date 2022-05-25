using Azure;
using Azure.Data.Tables;

namespace Infrastructure.TableService
{
    public class ScoreStateEntity : ITableEntity
    {
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; } //UserName
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public long NumberOwned { get; set; }
        public DateTime? LastPurchaseDateTime { get; set; }
        public long ScoreAfterLastPurchase { get; set; }
    }
}
