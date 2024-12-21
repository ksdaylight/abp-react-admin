using Volo.Abp.Domain.Entities;

namespace Miwen.Platform.Feedbacks;
public class FeedbackCommentUpdateDto : FeedbackCommentCreateOrUpdateDto, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
