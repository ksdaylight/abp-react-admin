namespace Miwen.Abp.Webhooks;

public interface IWebhookDefinitionProvider
{
    void Define(IWebhookDefinitionContext context);
}
