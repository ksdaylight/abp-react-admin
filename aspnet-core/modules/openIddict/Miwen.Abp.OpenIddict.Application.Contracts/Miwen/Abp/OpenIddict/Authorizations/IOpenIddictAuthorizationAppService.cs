using System;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.OpenIddict.Authorizations;

public interface IOpenIddictAuthorizationAppService :
    IReadOnlyAppService<
        OpenIddictAuthorizationDto,
        Guid,
        OpenIddictAuthorizationGetListInput>,
    IDeleteAppService<Guid>
{
}
