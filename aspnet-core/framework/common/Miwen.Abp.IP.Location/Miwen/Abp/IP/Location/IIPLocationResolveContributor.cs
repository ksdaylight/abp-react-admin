using System.Threading.Tasks;

namespace Miwen.Abp.IP.Location;
public interface IIPLocationResolveContributor
{
    string Name { get; }

    Task ResolveAsync(IIPLocationResolveContext context);
}
