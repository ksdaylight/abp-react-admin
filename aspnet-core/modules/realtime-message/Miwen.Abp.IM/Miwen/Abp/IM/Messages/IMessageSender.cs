using System.Threading.Tasks;

namespace Miwen.Abp.IM.Messages;

public interface IMessageSender
{
    Task<string> SendMessageAsync(ChatMessage chatMessage);
}
