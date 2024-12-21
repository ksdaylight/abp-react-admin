using System;

namespace Miwen.Abp.Wrapper;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class IgnoreWrapResultAttribute : Attribute
{
    public IgnoreWrapResultAttribute()
    {

    }
}
