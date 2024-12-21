using System;
using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.MessageService.Groups;

public class GroupRemoveUserDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public long GroupId { get; set; }
}
