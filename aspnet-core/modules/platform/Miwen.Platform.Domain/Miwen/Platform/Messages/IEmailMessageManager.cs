using System.Threading.Tasks;

namespace Miwen.Platform.Messages;
public interface IEmailMessageManager
{
    Task<EmailMessage> SendAsync(EmailMessage message);
}
