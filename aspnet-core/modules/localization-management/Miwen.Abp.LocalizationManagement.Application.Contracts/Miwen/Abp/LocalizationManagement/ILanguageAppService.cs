using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Miwen.Abp.LocalizationManagement;

public interface ILanguageAppService : IApplicationService
{
    Task<LanguageDto> GetByNameAsync(string name);

    Task<LanguageDto> CreateAsync(LanguageCreateDto input);

    Task<LanguageDto> UpdateAsync(string name, LanguageUpdateDto input);

    Task DeleteAsync(string name);
}
