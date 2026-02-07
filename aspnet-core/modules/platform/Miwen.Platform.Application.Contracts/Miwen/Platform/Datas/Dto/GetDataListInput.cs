using Volo.Abp.Application.Dtos;

namespace Miwen.Platform.Datas;

public class GetDataListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
