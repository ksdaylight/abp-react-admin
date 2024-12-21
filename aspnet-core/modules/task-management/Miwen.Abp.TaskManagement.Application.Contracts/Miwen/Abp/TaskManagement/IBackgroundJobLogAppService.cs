using Volo.Abp.Application.Services;

namespace Miwen.Abp.TaskManagement;

public interface IBackgroundJobLogAppService : 
    IReadOnlyAppService<
        BackgroundJobLogDto,
        long,
        BackgroundJobLogGetListInput>,
    IDeleteAppService<long>
{
}
