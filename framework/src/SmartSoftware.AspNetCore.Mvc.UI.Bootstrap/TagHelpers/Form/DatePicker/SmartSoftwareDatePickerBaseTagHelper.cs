using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form.DatePicker;

public abstract class
    SmartSoftwareDatePickerBaseTagHelper<TTagHelper> : SmartSoftwareTagHelper<TTagHelper, SmartSoftwareDatePickerBaseTagHelperService<TTagHelper>>, ISmartSoftwareDatePickerOptions
    where TTagHelper : SmartSoftwareDatePickerBaseTagHelper<TTagHelper>

{
    private ISmartSoftwareDatePickerOptions _ssDatePickerOptionsImplementation;

    public string? Label { get; set; }

    public string? LabelTooltip { get; set; }

    public string LabelTooltipIcon { get; set; } = "bi-info-circle";

    public string LabelTooltipPlacement { get; set; } = "right";

    public bool LabelTooltipHtml { get; set; } = false;

    [HtmlAttributeName("info")]
    public string? InfoText { get; set; }

    [HtmlAttributeName("disabled")]
    public bool IsDisabled { get; set; } = false;

    [HtmlAttributeName("readonly")]
    public bool? IsReadonly { get; set; } = false;

    public bool AutoFocus { get; set; }

    public SmartSoftwareFormControlSize Size { get; set; } = SmartSoftwareFormControlSize.Default;

    [HtmlAttributeName("required-symbol")]
    public bool DisplayRequiredSymbol { get; set; } = true;

    public string? Name { get; set; }

    public string? Value { get; set; }

    public bool SuppressLabel { get; set; }

    public bool AddMarginBottomClass  { get; set; } = true;

    protected SmartSoftwareDatePickerBaseTagHelper(SmartSoftwareDatePickerBaseTagHelperService<TTagHelper> service) : base(service)
    {
        _ssDatePickerOptionsImplementation = new SmartSoftwareDatePickerOptions();
    }

    public void SetDatePickerOptions(ISmartSoftwareDatePickerOptions options)
    {
        _ssDatePickerOptionsImplementation = options;
    }

    public string? PickerId {
        get => _ssDatePickerOptionsImplementation.PickerId;
        set => _ssDatePickerOptionsImplementation.PickerId = value;
    }

    public DateTime? MinDate {
        get => _ssDatePickerOptionsImplementation.MinDate;
        set => _ssDatePickerOptionsImplementation.MinDate = value;
    }

    public DateTime? MaxDate {
        get => _ssDatePickerOptionsImplementation.MaxDate;
        set => _ssDatePickerOptionsImplementation.MaxDate = value;
    }

    public object? MaxSpan {
        get => _ssDatePickerOptionsImplementation.MaxSpan;
        set => _ssDatePickerOptionsImplementation.MaxSpan = value;
    }

    public bool? ShowDropdowns {
        get => _ssDatePickerOptionsImplementation.ShowDropdowns;
        set => _ssDatePickerOptionsImplementation.ShowDropdowns = value;
    }

    public int? MinYear {
        get => _ssDatePickerOptionsImplementation.MinYear;
        set => _ssDatePickerOptionsImplementation.MinYear = value;
    }

    public int? MaxYear {
        get => _ssDatePickerOptionsImplementation.MaxYear;
        set => _ssDatePickerOptionsImplementation.MaxYear = value;
    }

    public SmartSoftwareDatePickerWeekNumbers WeekNumbers {
        get => _ssDatePickerOptionsImplementation.WeekNumbers;
        set => _ssDatePickerOptionsImplementation.WeekNumbers = value;
    }

    public bool? TimePicker {
        get => _ssDatePickerOptionsImplementation.TimePicker;
        set => _ssDatePickerOptionsImplementation.TimePicker = value;
    }

    public int? TimePickerIncrement {
        get => _ssDatePickerOptionsImplementation.TimePickerIncrement;
        set => _ssDatePickerOptionsImplementation.TimePickerIncrement = value;
    }

    public bool? TimePicker24Hour {
        get => _ssDatePickerOptionsImplementation.TimePicker24Hour;
        set => _ssDatePickerOptionsImplementation.TimePicker24Hour = value;
    }

    public bool? TimePickerSeconds {
        get => _ssDatePickerOptionsImplementation.TimePickerSeconds;
        set => _ssDatePickerOptionsImplementation.TimePickerSeconds = value;
    }

    public List<SmartSoftwareDatePickerRange>? Ranges {
        get => _ssDatePickerOptionsImplementation.Ranges;
        set => _ssDatePickerOptionsImplementation.Ranges = value;
    }

    public bool? ShowCustomRangeLabel {
        get => _ssDatePickerOptionsImplementation.ShowCustomRangeLabel;
        set => _ssDatePickerOptionsImplementation.ShowCustomRangeLabel = value;
    }

    public bool? AlwaysShowCalendars {
        get => _ssDatePickerOptionsImplementation.AlwaysShowCalendars;
        set => _ssDatePickerOptionsImplementation.AlwaysShowCalendars = value;
    }

    public SmartSoftwareDatePickerOpens Opens {
        get => _ssDatePickerOptionsImplementation.Opens;
        set => _ssDatePickerOptionsImplementation.Opens = value;
    }

    public SmartSoftwareDatePickerDrops Drops {
        get => _ssDatePickerOptionsImplementation.Drops;
        set => _ssDatePickerOptionsImplementation.Drops = value;
    }

    public string? ButtonClasses {
        get => _ssDatePickerOptionsImplementation.ButtonClasses;
        set => _ssDatePickerOptionsImplementation.ButtonClasses = value;
    }

    public string? TodayButtonClasses {
        get => _ssDatePickerOptionsImplementation.TodayButtonClasses;
        set => _ssDatePickerOptionsImplementation.TodayButtonClasses = value;
    }

    public string? ApplyButtonClasses {
        get => _ssDatePickerOptionsImplementation.ApplyButtonClasses;
        set => _ssDatePickerOptionsImplementation.ApplyButtonClasses = value;
    }

    public string? ClearButtonClasses {
        get => _ssDatePickerOptionsImplementation.ClearButtonClasses;
        set => _ssDatePickerOptionsImplementation.ClearButtonClasses = value;
    }

    public object? Locale {
        get => _ssDatePickerOptionsImplementation.Locale;
        set => _ssDatePickerOptionsImplementation.Locale = value;
    }

    public bool? AutoApply {
        get => _ssDatePickerOptionsImplementation.AutoApply;
        set => _ssDatePickerOptionsImplementation.AutoApply = value;
    }

    public bool? LinkedCalendars {
        get => _ssDatePickerOptionsImplementation.LinkedCalendars;
        set => _ssDatePickerOptionsImplementation.LinkedCalendars = value;
    }

    public bool? AutoUpdateInput {
        get => _ssDatePickerOptionsImplementation.AutoUpdateInput;
        set => _ssDatePickerOptionsImplementation.AutoUpdateInput = value;
    }

    public string? ParentEl {
        get => _ssDatePickerOptionsImplementation.ParentEl;
        set => _ssDatePickerOptionsImplementation.ParentEl = value;
    }

    [Obsolete("Use VisibleDateFormat instead.")]
    public string? DateFormat {
        get => _ssDatePickerOptionsImplementation.DateFormat;
        set => _ssDatePickerOptionsImplementation.DateFormat = value;
    }
    
    public string? VisibleDateFormat {
        get => _ssDatePickerOptionsImplementation.VisibleDateFormat;
        set => _ssDatePickerOptionsImplementation.VisibleDateFormat = value;
    }
    
    public string? InputDateFormat {
        get => _ssDatePickerOptionsImplementation.InputDateFormat;
        set => _ssDatePickerOptionsImplementation.InputDateFormat = value;
    }

    public bool OpenButton {
        get => _ssDatePickerOptionsImplementation.OpenButton;
        set => _ssDatePickerOptionsImplementation.OpenButton = value;
    }

    public bool? ClearButton {
        get => _ssDatePickerOptionsImplementation.ClearButton;
        set => _ssDatePickerOptionsImplementation.ClearButton = value;
    }

    public bool SingleOpenAndClearButton {
        get => _ssDatePickerOptionsImplementation.SingleOpenAndClearButton;
        set => _ssDatePickerOptionsImplementation.SingleOpenAndClearButton = value;
    }

    public bool? IsUtc {
        get => _ssDatePickerOptionsImplementation.IsUtc;
        set => _ssDatePickerOptionsImplementation.IsUtc = value;
    }

    public bool? IsIso {
        get => _ssDatePickerOptionsImplementation.IsIso;
        set => _ssDatePickerOptionsImplementation.IsIso = value;
    }

    public object? Options {
        get => _ssDatePickerOptionsImplementation.Options;
        set => _ssDatePickerOptionsImplementation.Options = value;
    }
}
