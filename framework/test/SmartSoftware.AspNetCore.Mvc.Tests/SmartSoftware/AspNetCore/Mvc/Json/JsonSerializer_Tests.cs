﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shouldly;
using SmartSoftware.Json;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.Json;

public class JsonSerializer_Tests : AspNetCoreMvcTestBase
{
    private readonly IJsonSerializer _jsonSerializer;

    public JsonSerializer_Tests()
    {
        _jsonSerializer = ServiceProvider.GetRequiredService<IJsonSerializer>();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<SmartSoftwareJsonOptions>(options =>
        {
            options.OutputDateTimeFormat = "yyyy*MM*dd";
        });

        base.ConfigureServices(services);
    }

    [Fact]
    public void DateFormatString_Test()
    {
        var output = _jsonSerializer.Serialize(new {
            Time = DateTime.Parse("2019-01-01 11:59:59")
        });

        output.ShouldContain("2019*01*01");
    }
}
