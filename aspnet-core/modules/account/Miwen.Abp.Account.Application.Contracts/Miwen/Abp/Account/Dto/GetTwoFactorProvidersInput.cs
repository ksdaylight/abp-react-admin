using System;
using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.Account;

public class GetTwoFactorProvidersInput
{
    [Required]
    public Guid UserId { get; set; }
}
