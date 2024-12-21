using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Platform.Datas;

public class GetDataByNameInput
{
    [Required]
    [DynamicStringLength(typeof(DataConsts), nameof(DataConsts.MaxNameLength))]
    public string Name { get; set; }
}
