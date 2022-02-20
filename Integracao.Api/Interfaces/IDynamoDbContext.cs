using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integracao.Api.Interfaces
{
    public interface IDynamoDbContext<T>
    {
        Task<IEnumerable<T>> ScanAsync(List<ScanCondition> conditions);
        Task<IEnumerable<T>> QueryAsync(string hashKey, QueryOperator op, IEnumerable<object> values);
        Task<IEnumerable<T>> QueryAsync(string hashKey);
        Task<T> GetByIdAsync(string hashKey, string rangeKey = "");
        Task Save(T item);
        Task DeleteByIdAsync(T item);
    }
}
