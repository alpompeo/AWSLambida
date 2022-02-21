using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWS.Utilities.Core.Dynamodb
{
    public class DynamoDbContext<T> : DynamoDBContext, IDynamoDbContext<T>
    {
        public DynamoDbContext(IAmazonDynamoDB client) : base(client)
        {
        }

        public async Task<IEnumerable<T>> ScanAsync(List<ScanCondition> conditions)
        {
            return await base.ScanAsync<T>(conditions).GetRemainingAsync();
        }

        public async Task<IEnumerable<T>> QueryAsync(string hashKey, QueryOperator op, IEnumerable<object> values)
        {
            return await base.QueryAsync<T>(hashKey, op, values).GetRemainingAsync();
        }

        public async Task<IEnumerable<T>> QueryAsync(string hashKey)
        {
            return await base.QueryAsync<T>(hashKey).GetRemainingAsync();
        }

        public async Task<T> GetByIdAsync(string hashKey, string rangeKey = "")
        {
            return await base.LoadAsync<T>(hashKey, rangeKey);
        }

        public async Task DeleteByIdAsync(T item)
        {
            await base.DeleteAsync(item);
        }

        public async Task Save(T item)
        {
            await base.SaveAsync(item);
        }
    }
}
