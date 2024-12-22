using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Miwen.Abp.LocalizationManagement.EntityFrameworkCore;
using Miwen.Abp.MessageService.EntityFrameworkCore;
using Miwen.Abp.Notifications.EntityFrameworkCore;
using Miwen.Abp.Saas.EntityFrameworkCore;
using Miwen.Abp.TaskManagement.EntityFrameworkCore;
using Miwen.Abp.TextTemplating.EntityFrameworkCore;
using Miwen.Abp.WebhooksManagement.EntityFrameworkCore;
using Miwen.Platform.EntityFrameworkCore;
using Miwen.Abp.DataProtectionManagement.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

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

        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureOpenIddict();
        modelBuilder.ConfigureSaas();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureTextTemplating();
        modelBuilder.ConfigureTaskManagement();
        modelBuilder.ConfigureWebhooksManagement();
        modelBuilder.ConfigurePlatform();
        modelBuilder.ConfigureLocalization();
        modelBuilder.ConfigureNotifications();
        modelBuilder.ConfigureNotificationsDefinition();
        modelBuilder.ConfigureMessageService();
        modelBuilder.ConfigureDataProtectionManagement();
    }
}