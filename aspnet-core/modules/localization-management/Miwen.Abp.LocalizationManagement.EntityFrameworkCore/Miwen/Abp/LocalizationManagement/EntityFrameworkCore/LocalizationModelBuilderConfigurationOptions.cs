using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Miwen.Abp.LocalizationManagement.EntityFrameworkCore;

public class LocalizationModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
{
    public LocalizationModelBuilderConfigurationOptions(
       [NotNull] string tablePrefix = "",
       [CanBeNull] string schema = null)
       : base(
           tablePrefix,
           schema)
    {

    }
}
