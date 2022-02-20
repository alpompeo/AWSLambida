using Amazon.SimpleNotificationService;
using Microsoft.Extensions.DependencyInjection;

namespace Sns.Message
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnsMessage(this IServiceCollection services)
        {

            services.AddScoped<ISnsMessage>(provider =>
                           new SnsMessage(new AmazonSimpleNotificationServiceClient()));
        }
    }
}
