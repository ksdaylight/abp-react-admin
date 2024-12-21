using Miwen.Platform.Datas;
using Miwen.Platform.Feedbacks;
using Miwen.Platform.Layouts;
using Miwen.Platform.Menus;
using Miwen.Platform.Packages;
using Miwen.Platform.Portal;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Miwen.Platform.EntityFrameworkCore;

[DependsOn(
    typeof(PlatformDomainModule),
    typeof(AbpEntityFrameworkCoreModule))]
public class PlatformEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<PlatformDbContext>(options =>
        {
            options.AddRepository<Data, EfCoreDataRepository>();
            options.AddRepository<Menu, EfCoreMenuRepository>();
            options.AddRepository<UserMenu, EfCoreUserMenuRepository>();
            options.AddRepository<RoleMenu, EfCoreRoleMenuRepository>();
            options.AddRepository<UserFavoriteMenu, EfCoreUserFavoriteMenuRepository>();
            options.AddRepository<Layout, EfCoreLayoutRepository>();
            options.AddRepository<Package, EfCorePackageRepository>();
            options.AddRepository<Enterprise, EfCoreEnterpriseRepository>();

            options.AddRepository<Feedback, EfCoreFeedbackRepository>();

            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}
