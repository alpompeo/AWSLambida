using System.Threading.Tasks;

namespace AWS.Utilities.Core.Sns.Sms
{
    public interface IAmazonSimpleNotificationServiceSms
    {
        Task<AmazonSimpleNotificationServiceResponse> SendMessageAsync(string phoneNumber, string message);
    }
}
