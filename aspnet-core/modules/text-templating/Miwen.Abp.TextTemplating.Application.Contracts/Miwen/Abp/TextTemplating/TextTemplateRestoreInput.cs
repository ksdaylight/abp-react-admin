using Volo.Abp.Validation;

namespace Miwen.Abp.TextTemplating;

public class TextTemplateRestoreInput
{
    [DynamicStringLength(typeof(TextTemplateConsts), nameof(TextTemplateConsts.MaxCultureLength))]
    public string? Culture { get; set; }
}
