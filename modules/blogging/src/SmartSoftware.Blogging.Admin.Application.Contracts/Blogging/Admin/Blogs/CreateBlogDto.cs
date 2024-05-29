using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Blogging.Admin.Blogs
{
    public class CreateBlogDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}
