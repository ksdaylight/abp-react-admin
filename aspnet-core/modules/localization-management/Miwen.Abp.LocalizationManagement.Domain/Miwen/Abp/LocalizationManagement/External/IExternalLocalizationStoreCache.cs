using System.Threading.Tasks;

namespace Miwen.Abp.LocalizationManagement.External;

public interface IExternalLocalizationStoreCache
{
    LocalizationResourceCacheItem GetResourceOrNull(string resourceName);

    Task<LocalizationResourceCacheItem> GetResourceOrNullAsync(string resourceName);

    Task<string[]> GetResourceNamesAsync();

    Task<LocalizationResourceCacheItem[]> GetResourcesAsync();
    
    Task RemoveAsync(string resourceName);
}
