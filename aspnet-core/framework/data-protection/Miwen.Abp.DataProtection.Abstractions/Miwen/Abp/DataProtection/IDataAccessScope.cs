using System;

namespace Miwen.Abp.DataProtection;

public interface IDataAccessScope
{
    DataAccessOperation[] Operations { get; }
    IDisposable BeginScope(DataAccessOperation[] operations = null);
}
