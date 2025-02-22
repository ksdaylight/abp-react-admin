using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Notifications;

public class UserNotificationGetByPagedDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }

    [DisplayName("Notifications:State")]
    public NotificationReadState? ReadState { get; set; }
}
