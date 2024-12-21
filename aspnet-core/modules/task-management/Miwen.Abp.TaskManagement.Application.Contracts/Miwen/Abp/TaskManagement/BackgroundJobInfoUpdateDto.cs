using Volo.Abp.Domain.Entities;

namespace Miwen.Abp.TaskManagement;

public class BackgroundJobInfoUpdateDto : BackgroundJobInfoCreateOrUpdateDto, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
