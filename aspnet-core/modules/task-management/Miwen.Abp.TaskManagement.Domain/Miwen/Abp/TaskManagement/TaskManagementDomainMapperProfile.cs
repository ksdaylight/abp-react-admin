using AutoMapper;
using Miwen.Abp.BackgroundTasks;

namespace Miwen.Abp.TaskManagement;

public class TaskManagementDomainMapperProfile : Profile
{
    public TaskManagementDomainMapperProfile()
    {
        CreateMap<BackgroundJobInfo, JobInfo>();
        CreateMap<BackgroundJobInfo, BackgroundJobEto>();
    }
}
