using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Abp.LocalizationManagement;

public class LanguageCreateDto : LanguageCreateOrUpdateDto
{
    [Required]
    [DynamicStringLength(typeof(LanguageConsts), nameof(LanguageConsts.MaxCultureNameLength))]
    public string CultureName { get; set; }

    [DynamicStringLength(typeof(LanguageConsts), nameof(LanguageConsts.MaxUiCultureNameLength))]
    public string UiCultureName { get; set; }
}
