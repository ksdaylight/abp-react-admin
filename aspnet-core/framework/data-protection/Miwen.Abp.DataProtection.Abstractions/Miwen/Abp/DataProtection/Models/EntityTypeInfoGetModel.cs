using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.DataProtection.Models;

public class EntityTypeInfoGetModel
{
    [Required]
    public DataAccessOperation Operation { get; set; }
}
