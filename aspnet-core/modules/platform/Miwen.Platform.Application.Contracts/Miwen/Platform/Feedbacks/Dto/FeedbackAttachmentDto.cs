using System;
using Volo.Abp.Application.Dtos;

namespace Miwen.Platform.Feedbacks;
public class FeedbackAttachmentDto : CreationAuditedEntityDto<Guid>
{
    public string Name { get; set; }
    public string Url { get; set; }
    public long Size { get; set; }
}
