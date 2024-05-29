using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartSoftware.Timing;

namespace SmartSoftware.EntityFrameworkCore.ValueConverters;

public class SmartSoftwareDateTimeValueConverter : ValueConverter<DateTime, DateTime>
{
    public SmartSoftwareDateTimeValueConverter(IClock clock, ConverterMappingHints? mappingHints = null)
        : base(
            x => clock.Normalize(x),
            x => clock.Normalize(x), mappingHints)
    {
    }
}

public class SmartSoftwareNullableDateTimeValueConverter : ValueConverter<DateTime?, DateTime?>
{
    public SmartSoftwareNullableDateTimeValueConverter(IClock clock, ConverterMappingHints? mappingHints = null)
        : base(
            x => x.HasValue ? clock.Normalize(x.Value) : x,
            x => x.HasValue ? clock.Normalize(x.Value) : x, mappingHints)
    {
    }
}
