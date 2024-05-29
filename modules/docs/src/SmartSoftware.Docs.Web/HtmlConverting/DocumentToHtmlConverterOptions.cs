using System;
using System.Collections.Generic;

namespace SmartSoftware.Docs.HtmlConverting
{
    public class DocumentToHtmlConverterOptions
    {
        public Dictionary<string, Type> Converters { get; set; }

        public DocumentToHtmlConverterOptions()
        {
            Converters = new Dictionary<string, Type>();
        }
    }
}
