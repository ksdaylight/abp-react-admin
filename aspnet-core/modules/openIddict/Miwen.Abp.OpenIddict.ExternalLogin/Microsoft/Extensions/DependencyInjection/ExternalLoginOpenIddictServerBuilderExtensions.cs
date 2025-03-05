using Miwen.Abp.OpenIddict.ExternalLogin;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExternalLoginOpenIddictServerBuilderExtensions
{
    public static OpenIddictServerBuilder AllowExternalLoginFlow(this OpenIddictServerBuilder builder)
    {
        return builder.AllowCustomFlow(ExternalLoginTokenExtensionGrantConsts.GrantType);
    }
}
