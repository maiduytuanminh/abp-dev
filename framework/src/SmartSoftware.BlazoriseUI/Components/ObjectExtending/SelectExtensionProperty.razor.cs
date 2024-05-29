﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using Blazorise;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.Data;
using SmartSoftware.Localization;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.BlazoriseUI.Components.ObjectExtending;

public partial class SelectExtensionProperty<TEntity, TResourceType>
    where TEntity : IHasExtraProperties
{
    protected List<SelectItem<int>> SelectItems = new();

    public int SelectedValue {
        get { return Entity.GetProperty<int>(PropertyInfo.Name); }
        set { Entity.SetProperty(PropertyInfo.Name, value, false); }
    }

    protected virtual List<SelectItem<int>> GetSelectItemsFromEnum()
    {
        var selectItems = new List<SelectItem<int>>();

        foreach (var enumValue in PropertyInfo.Type.GetEnumValues())
        {
            selectItems.Add(new SelectItem<int>
            {
                Value = (int)enumValue,
                Text = SmartSoftwareEnumLocalizer.GetString(PropertyInfo.Type, enumValue, new []{ StringLocalizerFactory.CreateDefaultOrNull() })
            });
        }

        return selectItems;
    }

    protected override void OnParametersSet()
    {
        SelectItems = GetSelectItemsFromEnum();
        StateHasChanged();

        if (!Entity.HasProperty(PropertyInfo.Name))
        {
            SelectedValue = (int)PropertyInfo.Type.GetEnumValues().GetValue(0)!;
        }
    }
}

public class SelectItem<TValue>
{
    public string Text { get; set; } = default!;
    public TValue Value { get; set; } = default!;
}
