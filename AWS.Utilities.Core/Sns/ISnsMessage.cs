using System.Threading.Tasks;

namespace AWS.Utilities.Core.Sns
{
    public interface ISnsMessage
    {
        Task<ResponseSns> SmsMessage(string phoneNumber, string message);
    }
}
