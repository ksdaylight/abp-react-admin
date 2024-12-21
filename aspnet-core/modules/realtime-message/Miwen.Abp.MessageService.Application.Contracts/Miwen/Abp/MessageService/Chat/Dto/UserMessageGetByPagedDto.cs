using Miwen.Abp.IM.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.MessageService.Chat;

public class UserMessageGetByPagedDto : PagedAndSortedResultRequestDto
{
    [Required]
    public Guid ReceiveUserId { get; set; }
    public string Filter { get; set; }
    public MessageType? MessageType { get; set; }
}
