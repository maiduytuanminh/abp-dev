using System;
using SmartSoftware.EventBus;

namespace SmartSoftware.CmsKit.Public.Comments;

[EventName("SmartSoftware.CmsKit.Comments.Created")]
public class CreatedCommentEvent
{
    public Guid Id { get; set; }
}
