using System.ComponentModel.DataAnnotations;
using SmartSoftware.Domain.Entities;

namespace SmartSoftware.Blogging.Admin.Blogs
{
    public class UpdateBlogDto : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        public string Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
