using System;

namespace Miwen.Abp.Wrapper;

public interface IExceptionWrapHandlerFactory
{
    IExceptionWrapHandler CreateFor(ExceptionWrapContext context);
}
