using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Miwen.Platform.Feedbacks;
public class FeedbackAttachmentGetInput
{
    [Required]
    public Guid FeedbackId { get; set; }

    [Required]
    [DynamicStringLength(typeof(FeedbackAttachmentConsts), nameof(FeedbackAttachmentConsts.MaxNameLength))]
    public string Name { get; set; }
}
