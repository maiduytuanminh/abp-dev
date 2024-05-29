using System.Collections.Generic;

namespace SmartSoftware.Docs.Documents
{
    public class DocumentParameterDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Dictionary<string, string> Values { get; set; }
    }
}