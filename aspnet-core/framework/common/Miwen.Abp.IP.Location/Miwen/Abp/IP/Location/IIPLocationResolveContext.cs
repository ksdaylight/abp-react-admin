using Volo.Abp.DependencyInjection;

namespace Miwen.Abp.IP.Location;
public interface IIPLocationResolveContext : IServiceProviderAccessor
{
    string IpAddress { get; }

    IPLocation? Location { get; set; }

    bool Handled { get; set; }
}
