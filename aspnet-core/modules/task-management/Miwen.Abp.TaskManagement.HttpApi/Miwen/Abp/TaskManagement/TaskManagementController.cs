using Miwen.Abp.TaskManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Miwen.Abp.TaskManagement;

public abstract class TaskManagementController : AbpControllerBase
{
    protected TaskManagementController()
    {
        LocalizationResource = typeof(TaskManagementResource);
    }
}
