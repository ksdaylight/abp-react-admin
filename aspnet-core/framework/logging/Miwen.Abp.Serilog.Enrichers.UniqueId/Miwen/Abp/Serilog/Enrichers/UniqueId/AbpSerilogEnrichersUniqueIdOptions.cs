using Miwen.Abp.IdGenerator.Snowflake;

namespace Miwen.Abp.Serilog.Enrichers.UniqueId;

public class AbpSerilogEnrichersUniqueIdOptions
{
    public SnowflakeIdOptions SnowflakeIdOptions { get; set; }
    public AbpSerilogEnrichersUniqueIdOptions()
    {
        SnowflakeIdOptions = new SnowflakeIdOptions();
    }
}
