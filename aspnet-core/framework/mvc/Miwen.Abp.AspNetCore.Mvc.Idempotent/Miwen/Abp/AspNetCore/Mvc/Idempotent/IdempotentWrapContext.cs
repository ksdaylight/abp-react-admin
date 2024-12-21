using Microsoft.AspNetCore.Mvc.Filters;

namespace Miwen.Abp.Idempotent;
public class IdempotentWrapContext
{
    public ActionExecutingContext ExecutingContext { get; }
    public AbpIdempotentOptions IdempotentOptions { get; }
    public IdempotentGrantResult GrantResult { get; }
    public IdempotentWrapContext(
        ActionExecutingContext executingContext,
        AbpIdempotentOptions idempotentOptions,
        IdempotentGrantResult grantResult)
    {
        ExecutingContext = executingContext;
        IdempotentOptions = idempotentOptions;
        GrantResult = grantResult;
    }
}
