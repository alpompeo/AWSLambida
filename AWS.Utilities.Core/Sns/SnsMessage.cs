using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
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

        public async Task<ResponseSns> SmsMessage(string phoneNumber, string message)
        {
            var messageSns = new ResponseSns
            {
                HasError = false
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
                messageSns.Message = ex.Message;
                messageSns.HasError = true;
            }

            return messageSns;
        }
    }
}
