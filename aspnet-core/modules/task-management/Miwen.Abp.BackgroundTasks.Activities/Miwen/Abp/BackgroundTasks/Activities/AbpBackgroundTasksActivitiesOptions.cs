using Volo.Abp.Collections;

namespace Miwen.Abp.BackgroundTasks.Activities;

public class AbpBackgroundTasksActivitiesOptions
{
    public ITypeList<IJobActionDefinitionProvider> DefinitionProviders { get; }

    public AbpBackgroundTasksActivitiesOptions()
    {
        DefinitionProviders = new TypeList<IJobActionDefinitionProvider>();
    }
}
