using System.ComponentModel.DataAnnotations;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Blogging.Comments.Dtos
{
    public class UpdateCommentDto : IHasConcurrencyStamp
    {
        [Required]
        public string Text { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
