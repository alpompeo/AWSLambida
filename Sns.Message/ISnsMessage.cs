using System.Threading.Tasks;

namespace Sns.Message
{
    public interface ISnsMessage
    {
        Task<string> SmsMessage(string phoneNumber, string message);
    }
}
