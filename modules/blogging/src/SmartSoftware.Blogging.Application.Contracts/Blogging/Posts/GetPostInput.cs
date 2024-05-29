 using System;
 using System.ComponentModel.DataAnnotations;

 namespace SmartSoftware.Blogging.Posts
{
    public class GetPostInput
    {
        [Required]
        public string Url { get; set; }

        public Guid BlogId { get; set; }
    }
}
