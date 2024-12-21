using Volo.Abp.Data;

namespace Miwen.Abp.WebhooksManagement.Definitions;

public class WebhookGroupDefinitionDto : IHasExtraProperties
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public bool IsStatic { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; set; } = new ExtraPropertyDictionary();
}
