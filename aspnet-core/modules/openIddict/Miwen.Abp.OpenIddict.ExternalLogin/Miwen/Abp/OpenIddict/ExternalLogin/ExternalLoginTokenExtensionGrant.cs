//using Miwen.Platform.Portal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using Volo.Abp.Security.Claims;
using Volo.Abp.Uow;
using Volo.Abp.Validation;
using static Volo.Abp.OpenIddict.Controllers.TokenController;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Miwen.Abp.OpenIddict.ExternalLogin;
public class ExternalLoginTokenExtensionGrant : ITokenExtensionGrant
{
    public string Name => ExternalLoginTokenExtensionGrantConsts.GrantType;

    protected IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    protected SignInManager<IdentityUser> SignInManager => LazyServiceProvider.LazyGetRequiredService<SignInManager<IdentityUser>>();
    protected IdentityUserManager UserManager => LazyServiceProvider.LazyGetRequiredService<IdentityUserManager>();
    protected IOpenIddictScopeManager ScopeManager => LazyServiceProvider.LazyGetRequiredService<IOpenIddictScopeManager>();
    protected AbpOpenIddictClaimsPrincipalManager OpenIddictClaimsPrincipalManager => LazyServiceProvider.LazyGetRequiredService<AbpOpenIddictClaimsPrincipalManager>();
    protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();
    protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);
    protected IServiceScopeFactory ServiceScopeFactory => LazyServiceProvider.LazyGetRequiredService<IServiceScopeFactory>();
    protected IOptions<AbpIdentityOptions> AbpIdentityOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<AbpIdentityOptions>>();
    protected IOptions<IdentityOptions> IdentityOptions => LazyServiceProvider.LazyGetRequiredService<IOptions<IdentityOptions>>();
    protected IdentitySecurityLogManager IdentitySecurityLogManager => LazyServiceProvider.LazyGetRequiredService<IdentitySecurityLogManager>();
    protected IdentityDynamicClaimsPrincipalContributorCache IdentityDynamicClaimsPrincipalContributorCache => LazyServiceProvider.LazyGetRequiredService<IdentityDynamicClaimsPrincipalContributorCache>();

    [UnitOfWork]
    public async virtual Task<IActionResult> HandleAsync(ExtensionGrantContext context)
    {
        LazyServiceProvider = context.HttpContext.RequestServices.GetRequiredService<IAbpLazyServiceProvider>();
               
        return await HandleExternalLoginAsync(context);
       
    }

    protected async virtual Task<IActionResult> HandleExternalLoginAsync(ExtensionGrantContext context)
    {
        using var scope = ServiceScopeFactory.CreateScope();
          
   
        await IdentityOptions.SetAsync();
        var loginInfo = await SignInManager.GetExternalLoginInfoAsync();

        if (loginInfo == null)
        {
            Logger.LogInformation("External login info is not available!");

            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "External login info is not available!"
            });

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var result = await SignInManager.ExternalLoginSignInAsync(
            loginInfo.LoginProvider,
            loginInfo.ProviderKey,
            isPersistent: false,
            bypassTwoFactor: true
        );

        if (!result.Succeeded)
        {
            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
            {
                Identity = OpenIddictSecurityLogIdentityConsts.OpenIddict,
                Action = "Login" + result,
            });

            string errorDescription;
            if (result.IsLockedOut)
            {
                Logger.LogInformation("Authentication failed , reason: locked out"); //TODO 检测中是否有携带Username
                errorDescription = "The user account has been locked out due to invalid login attempts. Please wait a while and try again.";
            }
            else if (result.IsNotAllowed)
            {
                Logger.LogInformation("Authentication failed , reason: not allowed");
                              
                errorDescription = "You are not allowed to login! Your account is inactive.";
            }
            else
            {
                Logger.LogInformation("Authentication failed, reason: invalid credentials");
                errorDescription = "Invalid username!";
            }

            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = errorDescription
            });

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        IdentityUser? user;
        if (result.Succeeded)
        {
            user = await UserManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey); //有过登录记录
            if (user != null)
            {
                await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);
                return await SetSuccessResultAsync(context, user); // 交由openiddict颁发token
            }
        }
        var email = loginInfo.Principal.FindFirstValue(AbpClaimTypes.Email) ?? loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
        if (email.IsNullOrWhiteSpace())
        {
            //return RedirectToPage("./Register", new
            //{
            //    IsExternalLogin = true,
            //    ExternalLoginAuthSchema = loginInfo.LoginProvider,
            //    ReturnUrl = returnUrl
            //});TODO 完成注册
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "TODO register"
            });
            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        user = await UserManager.FindByEmailAsync(email);
        if (user == null)
        {
            //return RedirectToPage("./Register", new
            //{
            //    IsExternalLogin = true,
            //    ExternalLoginAuthSchema = loginInfo.LoginProvider,
            //    ReturnUrl = returnUrl
            //});
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "TODO register"
            });
            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        if (await UserManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey) == null)
        {
            CheckIdentityErrors(await UserManager.AddLoginAsync(user, loginInfo));
        }

        await SignInManager.SignInAsync(user, false);

        await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext()
        {
            Identity = IdentitySecurityLogIdentityConsts.IdentityExternal,
            Action = result.ToIdentitySecurityLogAction(),
            UserName = user.Name
        });

        await IdentityDynamicClaimsPrincipalContributorCache.ClearAsync(user.Id, user.TenantId);


        return await SetSuccessResultAsync(context, user);
    }

 
 

    protected virtual async Task<IActionResult> SetSuccessResultAsync(ExtensionGrantContext context, IdentityUser user)
    {
        // Create a new ClaimsPrincipal containing the claims that
        // will be used to create an id_token, a token or a code.
        var principal = await SignInManager.CreateUserPrincipalAsync(user);

        principal.SetScopes(context.Request.GetScopes());
        principal.SetResources(await GetResourcesAsync(context.Request.GetScopes()));

        await SetClaimsDestinationsAsync(context, principal);

        await IdentitySecurityLogManager.SaveAsync(
            new IdentitySecurityLogContext
            {
                Identity = OpenIddictSecurityLogIdentityConsts.OpenIddict,
                Action = OpenIddictSecurityLogActionConsts.LoginSucceeded,
                UserName = user.Name,
                ClientId = context.Request.ClientId
            }
        );

        return new Microsoft.AspNetCore.Mvc.SignInResult(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
    }

    protected virtual ForbidResult? CheckIdentityErrors(IdentityResult identityResult)
    {
        if (!identityResult.Succeeded)
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "Operation failed: " + identityResult.Errors.Select(e => $"[{e.Code}] {e.Description}").JoinAsString(", ")
            });
            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        return null;
    }

    protected virtual async Task<IEnumerable<string>> GetResourcesAsync(ImmutableArray<string> scopes)
    {
        var resources = new List<string>();
        if (!scopes.Any())
        {
            return resources;
        }

        await foreach (var resource in ScopeManager.ListResourcesAsync(scopes))
        {
            resources.Add(resource);
        }
        return resources;
    }

    protected virtual async Task SetClaimsDestinationsAsync(ExtensionGrantContext context, ClaimsPrincipal principal)
    {
        await OpenIddictClaimsPrincipalManager.HandleAsync(context.Request, principal);
    }

    public virtual ForbidResult Forbid(AuthenticationProperties properties, params string[] authenticationSchemes)
    {
        return new ForbidResult(
            authenticationSchemes,
            properties);
    }
}
