using System.ComponentModel.DataAnnotations;

namespace Miwen.Abp.MessageService.Chat;

public class UserGroupGetByGroupIdDto
{
    [Required]
    public long GroupId { get; set; }
}
