using System.Threading;
using System.Threading.Tasks;

namespace Miwen.Abp.OpenApi;
public interface INonceStore
{
    Task<bool> TrySetAsync(string nonce, CancellationToken cancellationToken = default);
}
