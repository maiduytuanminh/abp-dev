﻿using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Timing;

namespace SmartSoftware.Json.SystemTextJson.JsonConverters;

public class SmartSoftwareNullableDateTimeConverter : JsonConverter<DateTime?>, ITransientDependency
{
    private readonly IClock _clock;
    private readonly SmartSoftwareJsonOptions _options;

    public SmartSoftwareNullableDateTimeConverter(IClock clock, IOptions<SmartSoftwareJsonOptions> ssJsonOptions)
    {
        _clock = clock;
        _options = ssJsonOptions.Value;
    }

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (_options.InputDateTimeFormats.Any())
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                foreach (var format in _options.InputDateTimeFormats)
                {
                    var s = reader.GetString();
                    if (DateTime.TryParseExact(s, format, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var d1))
                    {
                        return _clock.Normalize(d1);
                    }
                }
            }
            else
            {
                throw new JsonException("Reader's TokenType is not String!");
            }
        }

        if (reader.TryGetDateTime(out var d2))
        {
            return _clock.Normalize(d2);
        }

        var dateText = reader.GetString();
        if (!dateText.IsNullOrWhiteSpace())
        {
            if (DateTime.TryParse(dateText, CultureInfo.CurrentUICulture, DateTimeStyles.None, out var d3))
            {
                return _clock.Normalize(d3);
            }
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            if (_options.OutputDateTimeFormat.IsNullOrWhiteSpace())
            {
                writer.WriteStringValue(_clock.Normalize(value.Value));
            }
            else
            {
                writer.WriteStringValue(_clock.Normalize(value.Value).ToString(_options.OutputDateTimeFormat, CultureInfo.CurrentUICulture));
            }
        }
    }
}
