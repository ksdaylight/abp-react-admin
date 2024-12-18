using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.CachingManagement;

public class CacheKeyInput
{
    [Required]
    public string Key { get; set; }
}
