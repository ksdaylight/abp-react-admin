using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace Miwen.Abp.Identity.Session;

public interface IIdentitySessionChecker
{
    Task<bool> ValidateSessionAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken = default);
}
