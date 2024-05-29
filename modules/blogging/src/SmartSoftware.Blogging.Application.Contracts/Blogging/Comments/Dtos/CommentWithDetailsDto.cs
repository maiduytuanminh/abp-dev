using System;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Blogging.Posts;

namespace SmartSoftware.Blogging.Comments.Dtos
{
    public class CommentWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid? RepliedCommentId { get; set; }

        public string Text { get; set; }

        public BlogUserDto Writer { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
