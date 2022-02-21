using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System.Threading.Tasks;

namespace AWS.Utilities.Core.Sns
{
    public class SnsMessage : ISnsMessage
    {
        private readonly AmazonSimpleNotificationServiceClient _snsMessage;

        public SnsMessage(AmazonSimpleNotificationServiceClient snsMessage)
        {
            _snsMessage = snsMessage;
        }

        public async Task<string> SmsMessage(string phoneNumber, string message)
        {
            string messageError = string.Empty;

            try
            {
                var request = new PublishRequest
                {
                    Message = message,
                    PhoneNumber = phoneNumber,
                };

                await _snsMessage.PublishAsync(request);
            }
            catch (System.Exception ex)
            {
                messageError = ex.Message;
            }

            return messageError;
        }
    }
}
