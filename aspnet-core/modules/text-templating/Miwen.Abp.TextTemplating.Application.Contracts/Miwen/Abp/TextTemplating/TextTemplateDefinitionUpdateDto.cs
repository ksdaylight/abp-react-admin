using Volo.Abp.Domain.Entities;

namespace Miwen.Abp.TextTemplating;
public class TextTemplateDefinitionUpdateDto : TextTemplateDefinitionCreateOrUpdateDto, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
