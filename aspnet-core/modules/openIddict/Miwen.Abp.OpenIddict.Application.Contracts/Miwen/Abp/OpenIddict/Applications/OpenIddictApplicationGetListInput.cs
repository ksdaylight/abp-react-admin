using System;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.OpenIddict.Applications;

[Serializable]
public class OpenIddictApplicationGetListInput : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
