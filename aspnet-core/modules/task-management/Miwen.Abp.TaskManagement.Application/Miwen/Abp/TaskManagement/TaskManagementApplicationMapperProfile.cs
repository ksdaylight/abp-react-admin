using AutoMapper;

namespace Miwen.Abp.TaskManagement;

public class TaskManagementApplicationMapperProfile : Profile
{
    public TaskManagementApplicationMapperProfile()
    {
        CreateMap<BackgroundJobInfo, BackgroundJobInfoDto>();
        CreateMap<BackgroundJobLog, BackgroundJobLogDto>();
        CreateMap<BackgroundJobAction, BackgroundJobActionDto>();
    }
}
