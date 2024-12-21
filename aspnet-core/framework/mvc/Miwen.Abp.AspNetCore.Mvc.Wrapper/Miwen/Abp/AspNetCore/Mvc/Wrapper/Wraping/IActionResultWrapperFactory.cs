using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp.DependencyInjection;

namespace Miwen.Abp.AspNetCore.Mvc.Wrapper.Wraping;

public interface IActionResultWrapperFactory : ITransientDependency
{
    IActionResultWrapper CreateFor(FilterContext context);
}
