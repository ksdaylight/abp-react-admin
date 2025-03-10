namespace Miwen.Abp.OAuth.Github;

public class AbpOAuthGithubCacheItem
{
    public const string CacheKeyFormat = "pn:oauth,n:github";
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public AbpOAuthGithubCacheItem()
    {

    }

    public AbpOAuthGithubCacheItem(
        string clientId,
        string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }

    public static string CalculateCacheKey()
    {
        return CacheKeyFormat;
    }
}
