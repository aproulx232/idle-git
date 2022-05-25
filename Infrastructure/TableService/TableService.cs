using Azure.Data.Tables;

namespace Infrastructure.TableService
{
    public interface ITableService
    {
        Task<ScoreStateEntity> GetScoreState(string username);
    }

    public class TableService : ITableService
    {
        private readonly TableClient _tableClient;

        public TableService(TableServiceClient tableServiceClient, ITableServiceConfiguration tableServiceConfiguration)
        {
            if (tableServiceClient == null) throw new ArgumentNullException(nameof(tableServiceClient));
            if (tableServiceConfiguration == null) throw new ArgumentNullException(nameof(tableServiceConfiguration));

            tableServiceClient.CreateTableIfNotExists(tableServiceConfiguration.TableName);
            _tableClient = tableServiceClient.GetTableClient(tableServiceConfiguration.TableName);
        }

        public async Task<ScoreStateEntity> GetScoreState(string username)
        {
            var scoreStateEntity = (await _tableClient.GetEntityAsync<ScoreStateEntity>("DefaultPartition", username)).Value;

            if (scoreStateEntity != null) 
                return scoreStateEntity;

            scoreStateEntity = new ScoreStateEntity
            {
                PartitionKey = "DefaultPartition",
                RowKey = username
            };
            await _tableClient.AddEntityAsync(scoreStateEntity);

            return scoreStateEntity;
        }
    }
}
