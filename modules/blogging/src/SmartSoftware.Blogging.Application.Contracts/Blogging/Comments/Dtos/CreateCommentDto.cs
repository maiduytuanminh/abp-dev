using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Blogging.Comments.Dtos
{
    public class CreateCommentDto
    {
        public Guid? RepliedCommentId { get; set; }

        public Guid PostId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
