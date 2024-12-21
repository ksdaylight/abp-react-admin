using Volo.Abp.ExceptionHandling.Localization;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace Miwen.Abp.ExceptionHandling.Emailing.Templates;

public class ExceptionHandlingTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {
        context.Add(
           new TemplateDefinition(
               ExceptionHandlingTemplates.SendEmail,
               displayName: LocalizableString.Create<AbpExceptionHandlingResource>("TextTemplate:ExceptionHandlingTemplates.SendEmail"),
               defaultCultureName: "en"
           ).WithVirtualFilePath("/Miwen/Abp/ExceptionHandling/Emailing/Templates/SendEmail", false)
       );
    }
}
