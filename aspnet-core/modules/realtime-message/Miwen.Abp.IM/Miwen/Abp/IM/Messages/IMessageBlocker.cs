using System.Threading.Tasks;

namespace Miwen.Abp.IM.Messages;

/// <summary>
/// 消息拦截器
/// </summary>
public interface IMessageBlocker
{
    Task InterceptAsync(ChatMessage message);
}
