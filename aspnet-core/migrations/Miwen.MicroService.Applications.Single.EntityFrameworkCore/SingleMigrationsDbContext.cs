using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Miwen.MicroService.Applications.Single.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class SingleMigrationsDbContext : AbpDbContext<SingleMigrationsDbContext>
{
    public SingleMigrationsDbContext(DbContextOptions<SingleMigrationsDbContext> options)
    : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

        //
        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureBackgroundJobs();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureFeatureManagement();

        modelBuilder.ConfigureIdentity();

        modelBuilder.ConfigureOpenIddict();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureBlobStoring();

    }
}