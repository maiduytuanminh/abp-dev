using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Domain.Entities;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Comments;

namespace SmartSoftware.CmsKit.Public.Comments;

[Serializable]
public class UpdateCommentInput : ExtensibleObject, IHasConcurrencyStamp
{
    [Required]
    [DynamicStringLength(typeof(CommentConsts), nameof(CommentConsts.MaxTextLength))]
    public string Text { get; set; }

    public string ConcurrencyStamp { get; set; }
    
    public Guid? CaptchaToken { get; set; }
    
    public int CaptchaAnswer { get; set; }
}
