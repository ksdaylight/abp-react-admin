using System;

namespace Miwen.Abp.DataProtection;

public interface IJavaScriptTypeConvert
{
    JavaScriptTypeConvertResult Convert(Type propertyType);
}
