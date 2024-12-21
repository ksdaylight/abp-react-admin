using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Miwen.Abp.TextTemplating.EntityFrameworkCore;

[DependsOn(
    typeof(AbpTextTemplatingDomainModule),
    typeof(AbpEntityFrameworkCoreModule))]
public class AbpTextTemplatingEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TextTemplatingDbContext>(options =>
        {
            options.AddRepository<TextTemplate, EfCoreTextTemplateRepository>();
            options.AddRepository<TextTemplateDefinition, EfCoreTextTemplateDefinitionRepository>();

            options.AddDefaultRepositories<ITextTemplatingDbContext>();
        });
    }
}
