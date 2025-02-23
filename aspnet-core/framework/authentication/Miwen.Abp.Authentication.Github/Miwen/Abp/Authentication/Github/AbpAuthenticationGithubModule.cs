using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Volo.Abp.Modularity;
using static System.Formats.Asn1.AsnWriter;

namespace Miwen.Abp.Authentication.Github;

public class AbpAuthenticationGithubModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services
            .AddAuthentication()
            .AddGithubConnect(options => {
                // 用于防止初始化错误,会在OAuthHandler.InitializeHandlerAsync中进行重写
                options.ClientId = configuration["Authentication:GitHub:ClientId"]; //todo 
                options.ClientSecret = configuration["Authentication:GitHub:ClientSecret"];

                options.ClaimsIssuer = "GitHub";
                options.CallbackPath = new PathString(AbpAuthenticationGithubConsts.CallbackPath);


                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";


                options.Scope.Add("user:email");
                options.Scope.Add("read:user");

                // 这个原始的属性一定要写进去,框架关联判断是否绑定github
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");

                // 把自定义的身份标识写进令牌
                options.ClaimActions.MapJsonKey(AbpGithubClaimTypes.Id, "id");
                options.ClaimActions.MapJsonKey(AbpGithubClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey(AbpGithubClaimTypes.Avatar, "avatar_url");

            });
    }
}