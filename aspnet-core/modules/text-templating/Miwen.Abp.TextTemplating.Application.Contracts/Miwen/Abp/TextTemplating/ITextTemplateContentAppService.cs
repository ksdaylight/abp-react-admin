using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.TextTemplating;

public interface ITextTemplateContentAppService : IApplicationService
{
    Task<TextTemplateContentDto> GetAsync(TextTemplateContentGetInput input);

    Task RestoreToDefaultAsync(string name, TextTemplateRestoreInput input);

    Task<TextTemplateContentDto> UpdateAsync(string name, TextTemplateContentUpdateDto input);
}
