using System.Threading.Tasks;

namespace Miwen.Abp.BackgroundTasks;

public interface IJobEventTrigger
{
    Task OnJobBeforeExecuted(JobEventContext context);

    Task OnJobAfterExecuted(JobEventContext context);
}
