using Volo.Abp.Modularity;

using VoloAbpExceptionHandlingModule = Volo.Abp.ExceptionHandling.AbpExceptionHandlingModule
;
namespace Miwen.Abp.ExceptionHandling;

[DependsOn(typeof(VoloAbpExceptionHandlingModule))]
public class AbpExceptionHandlingModule : AbpModule
{
}
