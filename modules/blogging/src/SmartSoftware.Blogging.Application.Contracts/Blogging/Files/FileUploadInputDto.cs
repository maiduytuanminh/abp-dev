using System.ComponentModel.DataAnnotations;
using SmartSoftware.Content;

namespace SmartSoftware.Blogging.Files
{
    public class FileUploadInputDto
    {
        [Required]
        public IRemoteStreamContent File { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
