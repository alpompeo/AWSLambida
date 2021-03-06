using Amazon.DynamoDBv2;
using Amazon.SimpleNotificationService;
using AWS.Utilities.Core.Dynamodb;
using AWS.Utilities.Core.Sns.Sms;
using Microsoft.Extensions.DependencyInjection;

namespace AWS.Utilities.Core.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnsMessage(this IServiceCollection services)
        {
            services.AddScoped<IAmazonSimpleNotificationServiceSms>(provider => 
                    new AmazonSimpleNotificationServiceSms(new AmazonSimpleNotificationServiceClient()));
        }

        public static void AddDynamoDB<T>(this IServiceCollection services, AmazonDynamoDBConfig config)
        {
            services.AddScoped<IDynamoDbContext<T>>(provider => 
                    new DynamoDbContext<T>(new AmazonDynamoDBClient(config)));
        }
    }
}
