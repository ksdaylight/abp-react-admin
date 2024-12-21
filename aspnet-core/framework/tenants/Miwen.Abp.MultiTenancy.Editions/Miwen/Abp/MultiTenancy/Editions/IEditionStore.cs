using System;
using System.Threading.Tasks;

namespace Miwen.Abp.MultiTenancy.Editions;

public interface IEditionStore
{
    Task<EditionInfo> FindByTenantAsync(Guid tenantId);
}
