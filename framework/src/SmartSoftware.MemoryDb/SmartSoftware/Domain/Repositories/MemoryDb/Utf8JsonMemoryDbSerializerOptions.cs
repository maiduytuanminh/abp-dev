﻿using System.Text.Json;

namespace SmartSoftware.Domain.Repositories.MemoryDb;

public class Utf8JsonMemoryDbSerializerOptions
{
    public JsonSerializerOptions JsonSerializerOptions { get; }

    public Utf8JsonMemoryDbSerializerOptions()
    {
        JsonSerializerOptions = new JsonSerializerOptions();
    }
}
