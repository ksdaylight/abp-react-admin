using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.AspNetCore.Mvc.Localization;

public interface ITextAppService : IApplicationService
{
    Task<TextDto> GetByCultureKeyAsync(GetTextByKeyInput input);

    Task<ListResultDto<TextDifferenceDto>> GetListAsync(GetTextsInput input);
}
