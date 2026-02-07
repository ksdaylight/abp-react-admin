using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Saas.Editions;

public class EditionGetListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
