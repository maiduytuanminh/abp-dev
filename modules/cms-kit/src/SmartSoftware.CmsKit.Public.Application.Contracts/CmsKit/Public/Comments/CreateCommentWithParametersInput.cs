﻿using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Comments;

namespace SmartSoftware.CmsKit.Public.Comments;

[Serializable]
public class CreateCommentWithParametersInput
{
    [Required]
    [DynamicStringLength(typeof(CommentConsts), nameof(CommentConsts.MaxTextLength))]
    public string Text { get; set; }

    [Required]
    public string EntityType { get; set; }
    
    [Required]
    public string EntityId { get; set; }

    public Guid? RepliedCommentId { get; set; }
    
    public Guid? CaptchaToken { get; set; }
    
    public int CaptchaAnswer { get; set; }

    public string Url { get; set; }

    [Required]
    public string IdempotencyToken { get; set; }
}
