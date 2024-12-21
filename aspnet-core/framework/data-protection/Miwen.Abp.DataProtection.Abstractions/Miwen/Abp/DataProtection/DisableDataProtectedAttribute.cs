using System;

namespace Miwen.Abp.DataProtection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
public class DisableDataProtectedAttribute : Attribute
{
}
