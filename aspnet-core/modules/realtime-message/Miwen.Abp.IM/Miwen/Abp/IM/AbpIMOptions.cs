using Miwen.Abp.IM.Messages;
using Volo.Abp.Collections;

namespace Miwen.Abp.IM;

public class AbpIMOptions
{
    /// <summary>
    ///  消息发送者
    /// </summary>
    public ITypeList<IMessageSenderProvider> Providers { get; }

    public AbpIMOptions()
    {
        Providers = new TypeList<IMessageSenderProvider>();
    }
}
