using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Blogging.Posts
{
    public class UpdatePostDto : IHasConcurrencyStamp
    {
        public Guid BlogId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CoverImage { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Content { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
