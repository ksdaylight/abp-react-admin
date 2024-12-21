using System.Threading.Tasks;

namespace Miwen.Abp.Localization.Persistence;

public interface IStaticLocalizationSaver
{
    Task SaveAsync();
}
