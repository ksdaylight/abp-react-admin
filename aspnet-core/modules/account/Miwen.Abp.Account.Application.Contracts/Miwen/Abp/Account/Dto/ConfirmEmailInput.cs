using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.Account;

public class ConfirmEmailInput
{
    [Required]
    public string ConfirmToken { get; set; }
}
