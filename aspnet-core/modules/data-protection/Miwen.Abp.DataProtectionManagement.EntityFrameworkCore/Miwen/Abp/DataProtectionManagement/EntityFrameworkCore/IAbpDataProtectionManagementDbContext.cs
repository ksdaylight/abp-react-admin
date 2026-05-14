using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Miwen.Abp.DataProtectionManagement.EntityFrameworkCore;

public interface IAbpDataProtectionManagementDbContext : IEfCoreDbContext
{
    DbSet<EntityTypeInfo> EntityTypeInfos { get; set; }
}
