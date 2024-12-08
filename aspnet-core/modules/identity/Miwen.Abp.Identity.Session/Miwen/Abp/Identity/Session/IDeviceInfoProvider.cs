using System.Threading.Tasks;

namespace Miwen.Abp.Identity.Session;

public interface IDeviceInfoProvider
{
    Task<DeviceInfo> GetDeviceInfoAsync();

    string ClientIpAddress { get; }
}
