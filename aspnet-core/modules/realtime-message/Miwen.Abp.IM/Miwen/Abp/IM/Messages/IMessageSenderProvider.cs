using System.Threading.Tasks;

namespace Miwen.Abp.IM.Messages;

public interface IMessageSenderProvider
{
    string Name { get; }
    Task SendMessageAsync(ChatMessage chatMessage);
}
