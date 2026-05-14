using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.DataProtectionManagement;
public class GetEntityTypeInfoListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }

    public bool? IsAuditEnabled { get; set; }
}
