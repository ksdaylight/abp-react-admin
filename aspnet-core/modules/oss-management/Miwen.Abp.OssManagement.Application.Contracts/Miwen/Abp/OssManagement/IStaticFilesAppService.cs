using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Miwen.Abp.OssManagement;

public interface IStaticFilesAppService: IApplicationService
{
    Task<IRemoteStreamContent> GetAsync(GetStaticFileInput input);
}
