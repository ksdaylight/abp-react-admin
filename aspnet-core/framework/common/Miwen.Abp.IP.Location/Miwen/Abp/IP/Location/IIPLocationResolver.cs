using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Miwen.Abp.IP.Location;
public interface IIPLocationResolver
{
    [NotNull]
    Task<IPLocationResolveResult> ResolveAsync(string ipAddress);
}
