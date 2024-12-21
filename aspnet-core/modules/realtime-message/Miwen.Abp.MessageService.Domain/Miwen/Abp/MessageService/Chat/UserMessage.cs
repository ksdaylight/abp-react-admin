using Miwen.Abp.IM.Messages;
using System;

namespace Miwen.Abp.MessageService.Chat;

public class UserMessage : Message
{
    /// <summary>
    /// 接收用户标识
    /// </summary>
    public virtual Guid ReceiveUserId { get; set; }

    protected UserMessage() { }
    public UserMessage(
        long id,
        Guid sendUserId,
        string sendUserName,
        Guid receiveUserId,
        string content,
        MessageType type = MessageType.Text,
        MessageSourceType source = MessageSourceType.User,
        Guid? tenantId = null)
        : base(id, sendUserId, sendUserName, content, type, source, tenantId)
    {
        ReceiveUserId = receiveUserId;
    }
}
