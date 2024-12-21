using System;

namespace Miwen.Abp.Idempotent;

[AttributeUsage(AttributeTargets.Method)]
public class IgnoreIdempotentAttribute : Attribute
{
}
