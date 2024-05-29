using System.Collections.Generic;

namespace SmartSoftware.Blogging.Comments.Dtos
{
    public class CommentWithRepliesDto
    {
        public CommentWithDetailsDto Comment { get; set; }

        public List<CommentWithDetailsDto> Replies { get; set; } = new List<CommentWithDetailsDto>();
    }
}
