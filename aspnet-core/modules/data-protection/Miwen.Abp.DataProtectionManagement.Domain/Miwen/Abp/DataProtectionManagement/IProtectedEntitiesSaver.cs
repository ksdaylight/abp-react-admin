using System.Threading;
using System.Threading.Tasks;

namespace Miwen.Abp.DataProtectionManagement;
public interface IProtectedEntitiesSaver
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}
