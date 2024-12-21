using Volo.Abp.MultiTenancy;

namespace Miwen.MicroService.Applications.Single.MultiTenancy;

public interface ITenantConfigurationCache
{
    Task RefreshAsync();

    Task<List<TenantConfiguration>> GetTenantsAsync();
}
