using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;

namespace Miwen.Abp.Saas.Tenants;

public interface ITenantManager : IDomainService
{
    [NotNull]
    Task<Tenant> CreateAsync([NotNull] string name);

    Task ChangeNameAsync([NotNull] Tenant tenant, [NotNull] string name);
}
