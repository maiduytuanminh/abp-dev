using System.Collections.Generic;
using SmartSoftware.Content;

namespace SmartSoftware.TestApp.Application.Dto;

public class CreateMultipleFileInput
{
    public string Name { get; set; }

    public IEnumerable<RemoteStreamContent> Contents { get; set; }

    public CreateFileInput Inner { get; set; }
}
