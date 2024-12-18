using System.Threading.Tasks;

namespace Miwen.Abp.TextTemplating;
public interface IStaticTemplateSaver
{
    Task SaveDefinitionTemplateAsync();

    Task SaveTemplateContentAsync();
}
