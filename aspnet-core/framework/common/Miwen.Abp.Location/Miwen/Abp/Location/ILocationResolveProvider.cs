using System.Threading.Tasks;

namespace Miwen.Abp.Location;

public interface ILocationResolveProvider
{
    Task<IPGecodeLocation> IPGeocodeAsync(string ipAddress);

    Task<GecodeLocation> GeocodeAsync(string address, string city = null);

    Task<ReGeocodeLocation> ReGeocodeAsync(double lat, double lng, int radius = 50);
}
