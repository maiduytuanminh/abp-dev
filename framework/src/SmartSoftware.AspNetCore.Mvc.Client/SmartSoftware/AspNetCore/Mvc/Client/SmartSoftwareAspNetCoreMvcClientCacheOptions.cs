using System;

namespace SmartSoftware.AspNetCore.Mvc.Client;

public class SmartSoftwareAspNetCoreMvcClientCacheOptions
{
    public TimeSpan ApplicationConfigurationDtoCacheAbsoluteExpiration { get; set; }

    public SmartSoftwareAspNetCoreMvcClientCacheOptions()
    {
        ApplicationConfigurationDtoCacheAbsoluteExpiration = TimeSpan.FromSeconds(300);
    }
}
