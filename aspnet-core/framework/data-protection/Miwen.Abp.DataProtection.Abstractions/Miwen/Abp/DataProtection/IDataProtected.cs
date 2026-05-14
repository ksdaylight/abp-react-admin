using System;

namespace Miwen.Abp.DataProtection;

public interface IDataProtected
{
    Guid? CreatorId { get; }
}