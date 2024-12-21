using Miwen.Abp.BackgroundTasks;

namespace Miwen.Abp.TaskManagement;

public class BackgroundJobActionGetDefinitionsInput
{
    public JobActionType? Type { get; set; }
}
