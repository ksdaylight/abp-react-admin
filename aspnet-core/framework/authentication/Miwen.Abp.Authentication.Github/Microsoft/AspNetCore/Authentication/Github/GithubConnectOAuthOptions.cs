using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Miwen.Abp.Authentication.Github;
using System.Security.Claims;


namespace Microsoft.AspNetCore.Authentication.Github;

public class GithubConnectOAuthOptions : OAuthOptions
{
    public GithubConnectOAuthOptions()
    {
        ClientId = "GithubConnect";
        ClientSecret = "GithubConnect";

        ClaimsIssuer = "GitHub";
        CallbackPath = new PathString(AbpAuthenticationGithubConsts.CallbackPath);


        AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        TokenEndpoint = "https://github.com/login/oauth/access_token";
        UserInformationEndpoint = "https://api.github.com/user";


        Scope.Add("user:email");
        Scope.Add("read:user");

        // 这个原始的属性一定要写进去,框架关联判断是否绑定github
        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        ClaimActions.MapJsonKey(ClaimTypes.Name, "name");

        // 把自定义的身份标识写进令牌
        ClaimActions.MapJsonKey(AbpGithubClaimTypes.Id, "id");
        ClaimActions.MapJsonKey(AbpGithubClaimTypes.Email, "email");
        ClaimActions.MapJsonKey(AbpGithubClaimTypes.Avatar, "avatar_url");
    }
}