using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.MessageService.Groups;

public class GroupSearchInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
