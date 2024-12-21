using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Miwen.Abp.WebhooksManagement.Definitions;

public class WebhookGroupDefinitionUpdateDto : WebhookGroupDefinitionCreateOrUpdateDto, IHasConcurrencyStamp
{
    [StringLength(40)]
    public string ConcurrencyStamp { get; set; }
}
