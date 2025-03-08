using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.OpenIddict.Controllers;
using Volo.Abp.Identity;
using System.Linq;
using System;
using System.Web;

namespace Miwen.Abp.OpenIddict.AspNetCore.Controllers;

[Route("connect/external")]
[IgnoreAntiforgeryToken]
[ApiExplorerSettings(IgnoreApi = true)]
public class AccountExternalController : AbpOpenIdDictControllerBase
{
    protected IdentityDynamicClaimsPrincipalContributorCache IdentityDynamicClaimsPrincipalContributorCache => LazyServiceProvider.LazyGetRequiredService<IdentityDynamicClaimsPrincipalContributorCache>();


    /// <summary>
    /// 请求触发认证地址
    /// </summary>
    [HttpGet("login")]
    public async Task<IActionResult> ExternalLogin(string provider, string clientId)
    {
        //var redirectUrl = Url.Action("ExternalLoginCallback", "AccountExternal", new { provider, returnUrl }, Request.Scheme); //也就是配置成下面那个函数//虽然可以方便请求回调，但是只会进行get请求，opiddict拒绝对get请求返回token，最终还是要前端主动post

        // Retrieve the application details from the database.
        var application = await ApplicationManager.FindByClientIdAsync(clientId) ??
            throw new InvalidOperationException(L["DetailsConcerningTheCallingClientApplicationCannotBeFound"]);
        var cilentRedirctUri = await ApplicationManager.GetRedirectUrisAsync(application);

        //查找clietn的uri中，parameter中是否有provider，有的话就用这个uri
        var matchingUri = cilentRedirctUri.FirstOrDefault(uri =>
        {
            var queryParameters = HttpUtility.ParseQueryString(new Uri(uri).Query);
            return queryParameters["provider"] == provider;
        });
        //没有找到的话就用referer
        var baseUrl = matchingUri != null
        ? new Uri(matchingUri).GetLeftPart(UriPartial.Path)
        : Request.Headers["Referer"].ToString().Split('?')[0];

        //返回的url中加上provider和clientId
        var redirectUrl = $"{baseUrl}?provider={provider}&clientId={clientId}";

        //从provider中获取配置
        var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }
}
