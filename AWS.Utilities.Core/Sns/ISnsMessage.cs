using System.Threading.Tasks;

namespace AWS.Utilities.Core.Sns
{
    public interface ISnsMessage
    {
        Task<string> SmsMessage(string phoneNumber, string message);
    }
}
