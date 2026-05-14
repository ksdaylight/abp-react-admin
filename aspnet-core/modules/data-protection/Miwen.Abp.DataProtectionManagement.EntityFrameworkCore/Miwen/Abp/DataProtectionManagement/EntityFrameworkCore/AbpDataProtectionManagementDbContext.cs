using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Miwen.Abp.DataProtectionManagement.EntityFrameworkCore;
public class AbpDataProtectionManagementDbContext : AbpDbContext<AbpDataProtectionManagementDbContext>, IAbpDataProtectionManagementDbContext
{
    public virtual DbSet<EntityTypeInfo> EntityTypeInfos { get; set; }

    public AbpDataProtectionManagementDbContext(
        DbContextOptions<AbpDataProtectionManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDataProtectionManagement();
    }
}
