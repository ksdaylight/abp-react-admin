using Quartz;

namespace Miwen.Abp.BackgroundTasks.Quartz;

public interface IQuartzKeyBuilder
{
    JobKey CreateJobKey(JobInfo jobInfo);

    TriggerKey CreateTriggerKey(JobInfo jobInfo);
}
