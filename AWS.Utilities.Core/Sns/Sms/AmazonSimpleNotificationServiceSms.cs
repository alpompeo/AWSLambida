using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Threading.Tasks;

namespace AWS.Utilities.Core.Sns.Sms
{
    public class AmazonSimpleNotificationServiceSms : IAmazonSimpleNotificationServiceSms
    {
        private readonly AmazonSimpleNotificationServiceClient _snsMessage;

        public AmazonSimpleNotificationServiceSms(AmazonSimpleNotificationServiceClient snsMessage)
        {
            _snsMessage = snsMessage;
        }

        public async Task<AmazonSimpleNotificationServiceResponse> SendMessageAsync(string phoneNumber, string message)
        {
            var messageSns = new AmazonSimpleNotificationServiceResponse
            {
                HasError = false,
                MessageError = string.Empty
            };

            try
            {
                var request = new PublishRequest
                {
                    Message = message,
                    PhoneNumber = phoneNumber,
                };

                await _snsMessage.PublishAsync(request);
            }
            catch (Exception ex)
            {
                messageSns.MessageError = ex.Message;
                messageSns.HasError = true;
            }

            return messageSns;
        }
    }
}
