using Miwen.Abp.IM.Messages;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.MessageService.Chat;

public class GetUserLastMessageDto : ILimitedResultRequest, ISortedResultRequest
{
    public int MaxResultCount { get; set; }
    public string Sorting { get; set; }
    public MessageState? State { get; set; }
}
