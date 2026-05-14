using System.Threading.Tasks;

namespace Miwen.Abp.LocalizationManagement;

public interface IStaticLocalizationSaver
{
    Task SaveAsync();
}
