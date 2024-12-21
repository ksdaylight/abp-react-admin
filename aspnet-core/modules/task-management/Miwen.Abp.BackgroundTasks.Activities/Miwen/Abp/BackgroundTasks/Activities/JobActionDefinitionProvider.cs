using Volo.Abp.DependencyInjection;

namespace Miwen.Abp.BackgroundTasks.Activities;

public abstract class JobActionDefinitionProvider : IJobActionDefinitionProvider, ITransientDependency
{
    public abstract void Define(IJobActionDefinitionContext context);
}
