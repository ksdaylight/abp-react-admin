using Volo.Abp.DependencyInjection;

namespace Miwen.Abp.Webhooks;

public abstract class WebhookDefinitionProvider : IWebhookDefinitionProvider, ITransientDependency
{
    /// <summary>
    /// Used to add/manipulate webhook definitions.
    /// </summary>
    /// <param name="context">Context</param>,
    public abstract void Define(IWebhookDefinitionContext context);
}
