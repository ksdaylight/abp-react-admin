using System;

namespace Miwen.Abp.DataProtection;

public class DataAccessStrategyContributorContext
{
    public IServiceProvider ServiceProvider { get; }
    public DataAccessStrategyContributorContext(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}
