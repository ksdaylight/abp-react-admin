using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Abp.DataProtectionManagement;

public class SubjectStrategyGetInput
{
    [Required]
    [DynamicStringLength(typeof(SubjectStrategyConsts), nameof(SubjectStrategyConsts.MaxSubjectNameLength))]
    public string SubjectName {  get; set; }

    [Required]
    [DynamicStringLength(typeof(SubjectStrategyConsts), nameof(SubjectStrategyConsts.MaxSubjectIdLength))]
    public string SubjectId {  get; set; }
}
