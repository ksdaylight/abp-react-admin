using Miwen.Abp.TaskManagement.Localization;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.TaskManagement;

public abstract class TaskManagementApplicationService : ApplicationService
{
    protected TaskManagementApplicationService()
    {
        LocalizationResource = typeof(TaskManagementResource);
        ObjectMapperContext = typeof(TaskManagementApplicationModule);
    }
}
