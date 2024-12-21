using System.Collections.Generic;

namespace Miwen.Abp.IM.Messages;

public interface IMessageSenderProviderManager
{
    List<IMessageSenderProvider> Providers { get; }
}
