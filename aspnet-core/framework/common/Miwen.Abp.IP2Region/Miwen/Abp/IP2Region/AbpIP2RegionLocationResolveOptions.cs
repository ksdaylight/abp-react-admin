using Miwen.Abp.IP.Location;
using System;

namespace Miwen.Abp.IP2Region;
public class AbpIP2RegionLocationResolveOptions
{
    public Func<IPLocation, bool> UseCountry {  get; set; }
    public Func<IPLocation, bool> UseProvince {  get; set; }
    public AbpIP2RegionLocationResolveOptions()
    {
        UseCountry = _ => true;
        UseProvince = _ => true;
    }
}
