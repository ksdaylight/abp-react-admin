using System.Threading;
using Volo.Abp.BackgroundWorkers;

namespace Miwen.Abp.BackgroundTasks;

public interface IBackgroundWorkerRunnable : IJobRunnable
{
#nullable enable
    JobInfo? BuildWorker(IBackgroundWorker worker);
#nullable disable
}
