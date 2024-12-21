using System;
using Volo.Abp.Application.Dtos;

namespace Miwen.Platform.Feedbacks;
public class FeedbackCommentDto : AuditedEntityDto<Guid>
{
    public string Content { get; set; }
}
