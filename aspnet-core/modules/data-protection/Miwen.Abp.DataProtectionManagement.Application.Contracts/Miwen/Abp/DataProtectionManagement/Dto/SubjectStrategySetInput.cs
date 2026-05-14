using Miwen.Abp.DataProtection;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Abp.DataProtectionManagement;

public class SubjectStrategySetInput
{
    public bool IsEnabled { get; set; }

    [Required]
    [DynamicStringLength(typeof(SubjectStrategyConsts), nameof(SubjectStrategyConsts.MaxSubjectNameLength))]
    public string SubjectName { get; set; }

    [Required]
    [DynamicStringLength(typeof(SubjectStrategyConsts), nameof(SubjectStrategyConsts.MaxSubjectNameLength))]
    public string SubjectId { get; set; }

    public DataAccessStrategy Strategy { get; set; }
}
