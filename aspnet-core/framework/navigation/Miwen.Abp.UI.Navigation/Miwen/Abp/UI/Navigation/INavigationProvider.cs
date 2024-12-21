using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miwen.Abp.UI.Navigation;

public interface INavigationProvider
{
    Task<IReadOnlyCollection<ApplicationMenu>> GetAllAsync();
}
