using Volo.Abp.Domain.Entities;

namespace Miwen.Platform.Packages;

public class PackageUpdateDto : PackageCreateOrUpdateDto, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
