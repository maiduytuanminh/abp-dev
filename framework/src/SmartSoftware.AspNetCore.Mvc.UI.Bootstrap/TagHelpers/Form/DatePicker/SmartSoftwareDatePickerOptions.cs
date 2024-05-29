using System;
using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

public class SmartSoftwareDatePickerOptions : ISmartSoftwareDatePickerOptions
{
    public string? PickerId { get; set; }
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }
    public object? MaxSpan { get; set; }
    public bool? ShowDropdowns { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public SmartSoftwareDatePickerWeekNumbers WeekNumbers { get; set; } = SmartSoftwareDatePickerWeekNumbers.None;
    public bool? TimePicker { get; set; }
    public int? TimePickerIncrement { get; set; }
    public bool? TimePicker24Hour { get; set; }
    public bool? TimePickerSeconds { get; set; }
    public List<SmartSoftwareDatePickerRange>? Ranges { get; set; }
    public bool? ShowCustomRangeLabel { get; set; }
    public bool? AlwaysShowCalendars { get; set; }
    public SmartSoftwareDatePickerOpens Opens { get; set; } = SmartSoftwareDatePickerOpens.Center;
    public SmartSoftwareDatePickerDrops Drops { get; set; } = SmartSoftwareDatePickerDrops.Down;
    public string? ButtonClasses { get; set; }
    public string? TodayButtonClasses { get; set; }
    public string? ApplyButtonClasses { get; set; }
    public string? ClearButtonClasses { get; set; }
    public object? Locale { get; set; }
    public bool? AutoApply { get; set; }
    public bool? LinkedCalendars { get; set; }
    public bool? AutoUpdateInput { get; set; }
    public string? ParentEl { get; set; }
    
    [Obsolete("Use VisibleDateFormat instead.")]
    public string? DateFormat { get; set; }
    public string? VisibleDateFormat { get; set; }
    public string? InputDateFormat { get; set; }
    public bool OpenButton { get; set; } = true;
    public bool? ClearButton { get; set; }
    public bool SingleOpenAndClearButton { get; set; } = true;
    public bool? IsUtc { get; set; }
    public bool? IsIso { get; set; }
    public object? Options { get; set; }
}