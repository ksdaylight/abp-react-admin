using System.Threading.Tasks;

namespace Miwen.Platform.Messages;
public interface ISmsMessageManager
{
    Task<SmsMessage> SendAsync(SmsMessage message);
}
