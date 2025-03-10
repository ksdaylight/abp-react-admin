

namespace Miwen.Abp.OAuth.Github.Settings;

public class OAuthGithubSettingNames
{
    public static class GithubConnect
    {
        private const string Prefix = "Abp.OAuth" + ".GithubConnect"; //  Abp.OAuth  ..放到基项目

        public const string ClientId = Prefix + ".ClientId";
        public const string ClientSecret = Prefix + ".ClientSecret";
    }
}
