using Microsoft.AspNetCore.Mvc.Filters;

namespace Miwen.Abp.AspNetCore.Mvc.Wrapper.Wraping;

public interface IActionResultWrapper
{
    void Wrap(FilterContext context);
}
