namespace Miwen.Abp.OpenIddict.Applications;
public class OpenIddictApplicationSettingsDto
{
    public OpenIddictApplicationTokenLifetimesDto TokenLifetime { get; set; } = new OpenIddictApplicationTokenLifetimesDto();
}