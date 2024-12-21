using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Miwen.Abp.BackgroundTasks.Activities;

public interface IJobExecutedProvider
{
    Task NotifyErrorAsync([NotNull] JobActionExecuteContext context);
    Task NotifySuccessAsync([NotNull] JobActionExecuteContext context);
    Task NotifyComplateAsync([NotNull] JobActionExecuteContext context);
}
