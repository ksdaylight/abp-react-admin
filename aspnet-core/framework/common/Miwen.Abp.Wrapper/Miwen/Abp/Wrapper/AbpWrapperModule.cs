using Volo.Abp.ExceptionHandling;
using Volo.Abp.Modularity;

namespace Miwen.Abp.Wrapper;

[DependsOn(typeof(AbpExceptionHandlingModule))]
public class AbpWrapperModule: AbpModule
{

}
