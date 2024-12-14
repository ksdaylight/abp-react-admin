using System;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.OpenIddict.Scopes;

[Serializable]
public class OpenIddictScopeGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
