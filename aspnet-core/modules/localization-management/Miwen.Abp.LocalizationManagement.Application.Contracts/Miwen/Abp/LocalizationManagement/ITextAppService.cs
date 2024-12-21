using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.LocalizationManagement;

public interface ITextAppService : IApplicationService
{
    Task SetTextAsync(SetTextInput input);

    Task RestoreToDefaultAsync(RestoreDefaultTextInput input);
}
