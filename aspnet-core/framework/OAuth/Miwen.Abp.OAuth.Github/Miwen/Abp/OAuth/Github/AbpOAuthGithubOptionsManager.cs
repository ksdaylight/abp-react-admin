using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Miwen.Abp.OAuth.Github.Settings;
using System;
using System.Threading.Tasks;
using Volo.Abp.Options;
using Volo.Abp.Settings;

namespace Miwen.Abp.OAuth.Github;

public class AbpOAuthGithubOptionsManager : AbpDynamicOptionsManager<AbpOAuthGithubOptions>
{
    protected IMemoryCache OAuthGithubCache { get; }
    protected ISettingProvider SettingProvider { get; }
    public AbpOAuthGithubOptionsManager(
        IMemoryCache oAuthGithubCache,
        ISettingProvider settingProvider,
        IOptionsFactory<AbpOAuthGithubOptions> factory)
        : base(factory)
    {
        OAuthGithubCache = oAuthGithubCache;
        SettingProvider = settingProvider;
    }

    protected override async Task OverrideOptionsAsync(string name, AbpOAuthGithubOptions options)
    {
        var cacheItem = await GetCacheItemAsync();

        options.ClientId = cacheItem.ClientId;
        options.ClientSecret = cacheItem.ClientSecret;
    }

    protected async virtual Task<AbpOAuthGithubCacheItem> GetCacheItemAsync()
    {
        var cacheKey = AbpOAuthGithubCacheItem.CalculateCacheKey();

        var cacheItem = await OAuthGithubCache.GetOrCreateAsync(
            cacheKey,
            async (cache) =>
            {
                var clientId = await SettingProvider.GetOrNullAsync(OAuthGithubSettingNames.GithubConnect.ClientId);
                var clientSecret = await SettingProvider.GetOrNullAsync(OAuthGithubSettingNames.GithubConnect.ClientSecret);

                cache.SetAbsoluteExpiration(TimeSpan.FromMinutes(2d));

                return new AbpOAuthGithubCacheItem(clientId, clientSecret);
            });

        return cacheItem;
    }
}
