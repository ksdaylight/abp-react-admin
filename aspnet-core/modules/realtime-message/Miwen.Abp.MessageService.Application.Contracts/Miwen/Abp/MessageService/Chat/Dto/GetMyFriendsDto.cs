using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.MessageService.Chat;

public class GetMyFriendsDto : ISortedResultRequest
{
    public string Sorting { get; set; }
}
