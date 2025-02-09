using System.Threading.Tasks;

namespace Miwen.Abp.IP.Location;
public abstract class IPLocationResolveContributorBase : IIPLocationResolveContributor
{
    public abstract string Name { get; }

    public abstract Task ResolveAsync(IIPLocationResolveContext context);
}
