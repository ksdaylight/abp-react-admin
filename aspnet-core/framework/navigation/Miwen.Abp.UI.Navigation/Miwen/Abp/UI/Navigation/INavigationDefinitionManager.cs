using System.Collections.Generic;

namespace Miwen.Abp.UI.Navigation;

public interface INavigationDefinitionManager
{
    IReadOnlyList<NavigationDefinition> GetAll();
}
