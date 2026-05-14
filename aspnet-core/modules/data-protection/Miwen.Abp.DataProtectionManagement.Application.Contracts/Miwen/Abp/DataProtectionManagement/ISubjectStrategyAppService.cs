using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.DataProtectionManagement;

public interface ISubjectStrategyAppService : IApplicationService
{
    Task<SubjectStrategyDto> GetAsync(SubjectStrategyGetInput input);

    Task<SubjectStrategyDto> SetAsync(SubjectStrategySetInput input);
}
