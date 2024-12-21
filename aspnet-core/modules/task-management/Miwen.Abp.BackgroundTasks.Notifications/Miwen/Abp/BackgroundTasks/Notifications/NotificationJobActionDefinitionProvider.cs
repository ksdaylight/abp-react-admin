using Miwen.Abp.BackgroundTasks.Activities;
using Miwen.Abp.BackgroundTasks.Localization;
using Volo.Abp.Localization;

namespace Miwen.Abp.BackgroundTasks.Notifications;

public class NotificationJobActionDefinitionProvider : JobActionDefinitionProvider
{
    public override void Define(IJobActionDefinitionContext context)
    {
        context.Add(
            new JobActionDefinition(
                JobExecutedSuccessedNotificationProvider.Name,
                JobActionType.Successed,
                L("Notifications:JobExecuteSucceeded"),
                NotificationJobExecutedProvider.Paramters,
                L("Notifications:JobExecuteSucceededDesc"))
            .WithProvider<JobExecutedSuccessedNotificationProvider>());
        context.Add(
            new JobActionDefinition(
                JobExecuteFailedNotificationProvider.Name,
                JobActionType.Failed,
                L("Notifications:JobExecuteFailed"),
                NotificationJobExecutedProvider.Paramters,
                L("Notifications:JobExecuteFailedDesc"))
            .WithProvider<JobExecuteFailedNotificationProvider>());
        context.Add(
            new JobActionDefinition(
                JobExecuteCompletedNotificationProvider.Name,
                JobActionType.Completed,
                L("Notifications:JobExecuteCompleted"),
                NotificationJobExecutedProvider.Paramters,
                L("Notifications:JobExecuteCompletedDesc"))
            .WithProvider<JobExecuteCompletedNotificationProvider>());
    }

    private static ILocalizableString L(string name)
    {
        return LocalizableString.Create<BackgroundTasksResource>(name);
    }
}
