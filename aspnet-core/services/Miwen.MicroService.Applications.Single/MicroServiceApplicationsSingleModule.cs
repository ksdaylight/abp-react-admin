using Miwen.Abp.Account;
using Miwen.Abp.Account.Templates;
using Miwen.Abp.AspNetCore.HttpOverrides;
using Miwen.Abp.AspNetCore.Mvc.Idempotent.Wrapper;
using Miwen.Abp.AspNetCore.Mvc.Localization;
using Miwen.Abp.AspNetCore.Mvc.Wrapper;
using Miwen.Abp.Auditing;
using Miwen.Abp.AuditLogging.EntityFrameworkCore;
using Miwen.Abp.Authorization.OrganizationUnits;
using Miwen.Abp.BackgroundTasks;
using Miwen.Abp.BackgroundTasks.Activities;
using Miwen.Abp.BackgroundTasks.DistributedLocking;
using Miwen.Abp.BackgroundTasks.EventBus;
using Miwen.Abp.BackgroundTasks.ExceptionHandling;
using Miwen.Abp.BackgroundTasks.Jobs;
using Miwen.Abp.BackgroundTasks.Notifications;
using Miwen.Abp.BackgroundTasks.Quartz;
using Miwen.Abp.CachingManagement;
using Miwen.Abp.CachingManagement.StackExchangeRedis;
using Miwen.Abp.Dapr.Client;
using Miwen.Abp.DataProtectionManagement;
using Miwen.Abp.DataProtectionManagement.EntityFrameworkCore;
using Miwen.Abp.Elsa;
using Miwen.Abp.Elsa.Activities;
using Miwen.Abp.Elsa.EntityFrameworkCore;
using Miwen.Abp.Elsa.EntityFrameworkCore.PostgreSql;
using Miwen.Abp.ExceptionHandling;
using Miwen.Abp.ExceptionHandling.Emailing;
using Miwen.Abp.Exporter.MiniExcel;
using Miwen.Abp.FeatureManagement;
using Miwen.Abp.FeatureManagement.HttpApi;
using Miwen.Abp.Features.LimitValidation;
using Miwen.Abp.Features.LimitValidation.Redis.Client;
using Miwen.Abp.Http.Client.Wrapper;
using Miwen.Abp.Identity;
using Miwen.Abp.Identity.AspNetCore.Session;
using Miwen.Abp.Identity.EntityFrameworkCore;
using Miwen.Abp.Identity.Notifications;
using Miwen.Abp.Identity.OrganizaztionUnits;
using Miwen.Abp.Identity.Session.AspNetCore;
using Miwen.Abp.IdGenerator;
using Miwen.Abp.IM.SignalR;
using Miwen.Abp.Localization.CultureMap;
using Miwen.Abp.Localization.Persistence;
using Miwen.Abp.LocalizationManagement;
using Miwen.Abp.LocalizationManagement.EntityFrameworkCore;
using Miwen.Abp.MessageService;
using Miwen.Abp.MessageService.EntityFrameworkCore;
using Miwen.Abp.MultiTenancy.Editions;
using Miwen.Abp.Notifications;
using Miwen.Abp.Notifications.Common;
using Miwen.Abp.Notifications.Emailing;
using Miwen.Abp.Notifications.EntityFrameworkCore;
using Miwen.Abp.Notifications.SignalR;
using Miwen.Abp.OpenApi.Authorization;
using Miwen.Abp.OpenIddict;
using Miwen.Abp.OpenIddict.AspNetCore;
using Miwen.Abp.OpenIddict.AspNetCore.Session;
using Miwen.Abp.OpenIddict.Portal;
using Miwen.Abp.OpenIddict.Sms;
using Miwen.Abp.OssManagement;
using Miwen.Abp.OssManagement.FileSystem;
using Miwen.Abp.OssManagement.Imaging;
using Miwen.Abp.OssManagement.Minio;
using Miwen.Abp.OssManagement.SettingManagement;
using Miwen.Abp.PermissionManagement;
using Miwen.Abp.PermissionManagement.HttpApi;
using Miwen.Abp.PermissionManagement.OrganizationUnits;
using Miwen.Abp.Saas;
using Miwen.Abp.Saas.EntityFrameworkCore;
using Miwen.Abp.Serilog.Enrichers.Application;
using Miwen.Abp.Serilog.Enrichers.UniqueId;
using Miwen.Abp.SettingManagement;
using Miwen.Abp.TaskManagement;
using Miwen.Abp.TaskManagement.EntityFrameworkCore;
using Miwen.Abp.TextTemplating;
using Miwen.Abp.TextTemplating.EntityFrameworkCore;
using Miwen.Abp.UI.Navigation;
using Miwen.Abp.UI.Navigation.VueVbenAdmin;
using Miwen.Abp.Webhooks;
using Miwen.Abp.Webhooks.EventBus;
using Miwen.Abp.Webhooks.Identity;
using Miwen.Abp.Webhooks.Saas;
using Miwen.Abp.WebhooksManagement;
using Miwen.Abp.WebhooksManagement.EntityFrameworkCore;
using Miwen.Platform;
using Miwen.Platform.EntityFrameworkCore;
using Miwen.Platform.HttpApi;
using Miwen.Platform.Settings.VueVbenAdmin;
using Miwen.Platform.Theme.VueVbenAdmin;
using Miwen.MicroService.Applications.Single.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EventBus;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Imaging;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.OpenIddict;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Threading;
using Volo.Abp.Timing;
using Volo.Abp.MailKit;


namespace Miwen.MicroService.Applications.Single;

[DependsOn(
    typeof(AbpAccountApplicationModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAuditingApplicationModule),
    typeof(AbpAuditingHttpApiModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpCachingManagementStackExchangeRedisModule),
    typeof(AbpCachingManagementApplicationModule),
    typeof(AbpCachingManagementHttpApiModule),
    typeof(AbpIdentityAspNetCoreSessionModule),
    typeof(AbpIdentitySessionAspNetCoreModule),
    typeof(AbpIdentityNotificationsModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpLocalizationManagementDomainModule),
    typeof(AbpLocalizationManagementApplicationModule),
    typeof(AbpLocalizationManagementHttpApiModule),
    typeof(AbpLocalizationManagementEntityFrameworkCoreModule),
    typeof(AbpSerilogEnrichersApplicationModule),
    typeof(AbpSerilogEnrichersUniqueIdModule),
    typeof(AbpMessageServiceDomainModule),
    typeof(AbpMessageServiceApplicationModule),
    typeof(AbpMessageServiceHttpApiModule),
    typeof(AbpMessageServiceEntityFrameworkCoreModule),
    typeof(AbpNotificationsDomainModule),
    typeof(AbpNotificationsApplicationModule),
    typeof(AbpNotificationsHttpApiModule),
    typeof(AbpNotificationsEntityFrameworkCoreModule),


    typeof(AbpOpenIddictAspNetCoreModule),
    typeof(AbpOpenIddictAspNetCoreSessionModule),
    typeof(AbpOpenIddictApplicationModule),
    typeof(AbpOpenIddictHttpApiModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpOpenIddictSmsModule),
    typeof(AbpOpenIddictPortalModule),

    //typeof(AbpOssManagementMinioModule), // 取消注释以使用Minio
    typeof(AbpOssManagementFileSystemModule),
    typeof(AbpOssManagementImagingModule),
    typeof(AbpOssManagementDomainModule),
    typeof(AbpOssManagementApplicationModule),
    typeof(AbpOssManagementHttpApiModule),
    typeof(AbpOssManagementSettingManagementModule),
    typeof(AbpImagingImageSharpModule),

    typeof(PlatformDomainModule),
    typeof(PlatformApplicationModule),
    typeof(PlatformHttpApiModule),
    typeof(PlatformEntityFrameworkCoreModule),
    typeof(PlatformSettingsVueVbenAdminModule),
    typeof(PlatformThemeVueVbenAdminModule),
    typeof(AbpUINavigationVueVbenAdminModule),

    typeof(AbpSaasDomainModule),
    typeof(AbpSaasApplicationModule),
    typeof(AbpSaasHttpApiModule),
    typeof(AbpSaasEntityFrameworkCoreModule),

    typeof(TaskManagementDomainModule),
    typeof(TaskManagementApplicationModule),
    typeof(TaskManagementHttpApiModule),
    typeof(TaskManagementEntityFrameworkCoreModule),

    typeof(AbpTextTemplatingDomainModule),
    typeof(AbpTextTemplatingApplicationModule),
    typeof(AbpTextTemplatingHttpApiModule),
    typeof(AbpTextTemplatingEntityFrameworkCoreModule),

    typeof(AbpWebhooksModule),
    typeof(AbpWebhooksEventBusModule),
    typeof(AbpWebhooksIdentityModule),
    typeof(AbpWebhooksSaasModule),
    typeof(WebhooksManagementDomainModule),
    typeof(WebhooksManagementApplicationModule),
    typeof(WebhooksManagementHttpApiModule),
    typeof(WebhooksManagementEntityFrameworkCoreModule),

    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),

    typeof(AbpSettingManagementDomainModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),

    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementDomainOrganizationUnitsModule), // 组织机构权限管理

     typeof(AbpEntityFrameworkCorePostgreSqlModule),


    typeof(AbpAuthorizationOrganizationUnitsModule),
    typeof(AbpIdentityOrganizaztionUnitsModule),

    typeof(AbpBackgroundTasksModule),
    typeof(AbpBackgroundTasksActivitiesModule),
    typeof(AbpBackgroundTasksDistributedLockingModule),
    typeof(AbpBackgroundTasksEventBusModule),
    typeof(AbpBackgroundTasksExceptionHandlingModule),
    typeof(AbpBackgroundTasksJobsModule),
    typeof(AbpBackgroundTasksNotificationsModule),
    typeof(AbpBackgroundTasksQuartzModule),

    typeof(AbpDataProtectionManagementApplicationModule),
    typeof(AbpDataProtectionManagementHttpApiModule),
    typeof(AbpDataProtectionManagementEntityFrameworkCoreModule),


    typeof(AbpDaprClientModule),
    typeof(AbpExceptionHandlingModule),
    typeof(AbpEmailingExceptionHandlingModule),
    typeof(AbpFeaturesLimitValidationModule),
    typeof(AbpFeaturesValidationRedisClientModule),
    typeof(AbpAspNetCoreMvcLocalizationModule),

    typeof(AbpLocalizationCultureMapModule),
    typeof(AbpLocalizationPersistenceModule),

    typeof(AbpOpenApiAuthorizationModule),

    typeof(AbpIMSignalRModule),

    typeof(AbpNotificationsModule),
    typeof(AbpNotificationsCommonModule),
    typeof(AbpNotificationsSignalRModule),
    typeof(AbpNotificationsEmailingModule),
    typeof(AbpMultiTenancyEditionsModule),



    typeof(AbpIdGeneratorModule),
    typeof(AbpUINavigationModule),
    typeof(AbpAccountTemplatesModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    typeof(AbpCachingStackExchangeRedisModule),

     typeof(AbpElsaModule),
     typeof(AbpElsaServerModule),
     typeof(AbpElsaActivitiesModule),
     typeof(AbpElsaEntityFrameworkCoreModule),
     typeof(AbpElsaEntityFrameworkCorePostgreSqlModule),

    typeof(AbpExporterMiniExcelModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpHttpClientWrapperModule),
    typeof(AbpAspNetCoreMvcWrapperModule),
    typeof(AbpAspNetCoreMvcIdempotentWrapperModule),
    typeof(AbpAspNetCoreHttpOverridesModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpMailKitModule),
    typeof(AbpEventBusModule),
    typeof(AbpAutofacModule)
    )]
public partial class MicroServiceApplicationsSingleModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureDbContext();

        PreConfigureWrapper();
        PreConfigureFeature();
        PreConfigureIdentity();
        PreConfigureApp(configuration);
        PreConfigureQuartz(configuration);
        PreConfigureAuthServer(configuration);
        PreConfigureElsa(context.Services, configuration);
        PreConfigureCertificate(configuration, hostingEnvironment);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });

        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        //ConfigureWeChat();
        ConfigureWrapper();
        ConfigureExporter();
        ConfigureAuditing();
        //ConfigureDbContext();
        ConfigureIdempotent();
        ConfigureMvcUiTheme();
        ConfigureDataSeeder();
        ConfigureLocalization();
        ConfigureKestrelServer();
        ConfigureBackgroundTasks();
        ConfigureExceptionHandling();
        ConfigureVirtualFileSystem();
        ConfigureEntityDataProtected();
        ConfigureUrls(configuration);
        ConfigureCaching(configuration);
        ConfigureAuditing(configuration);
        ConfigureIdentity(configuration);
        ConfigureAuthServer(configuration);
        ConfigureSwagger(context.Services);
        ConfigureEndpoints(context.Services);
        ConfigureBlobStoring(configuration);
        ConfigureMultiTenancy(configuration);
        ConfigureJsonSerializer(configuration);
        ConfigureTextTemplating(configuration);
        ConfigureFeatureManagement(configuration);
        ConfigureSettingManagement(configuration);
        ConfigureWebhooksManagement(configuration);
        ConfigurePermissionManagement(configuration);
        ConfigureNotificationManagement(configuration);
        ConfigureCors(context.Services, configuration);
        ConfigureDistributedLock(context.Services, configuration);
        ConfigureSecurity(context.Services, configuration, hostingEnvironment.IsDevelopment());
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(async () => await OnApplicationInitializationAsync(context));
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider.GetRequiredService<IDataSeeder>().SeedAsync(); ;
    }
}
