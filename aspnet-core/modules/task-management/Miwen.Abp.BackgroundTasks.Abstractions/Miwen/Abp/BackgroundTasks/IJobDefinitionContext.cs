using System.Collections.Generic;

namespace Miwen.Abp.BackgroundTasks;

public interface IJobDefinitionContext
{
    JobDefinition GetOrNull(string name);

    IReadOnlyList<JobDefinition> GetAll();

    void Add(params JobDefinition[] definitions);
}
