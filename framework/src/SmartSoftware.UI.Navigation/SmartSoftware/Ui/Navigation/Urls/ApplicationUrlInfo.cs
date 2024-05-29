﻿using System.Collections.Generic;

namespace SmartSoftware.UI.Navigation.Urls;

public class ApplicationUrlInfo
{
    public string? RootUrl { get; set; }

    public IDictionary<string, string> Urls { get; }

    public ApplicationUrlInfo()
    {
        Urls = new Dictionary<string, string>();
    }
}
