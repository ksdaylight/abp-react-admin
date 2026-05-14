using Volo.Abp.EventBus;

namespace Miwen.Platform.Messages;

[EventName("platform.messages.sms")]
public class SmsMessageEto : MessageEto
{
}
