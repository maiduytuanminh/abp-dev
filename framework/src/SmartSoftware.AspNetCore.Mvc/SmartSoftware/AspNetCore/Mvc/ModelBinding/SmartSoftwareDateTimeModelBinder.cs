using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SmartSoftware.Timing;

namespace SmartSoftware.AspNetCore.Mvc.ModelBinding;

public class SmartSoftwareDateTimeModelBinder : IModelBinder
{
    private readonly DateTimeModelBinder _dateTimeModelBinder;
    private readonly IClock _clock;

    public SmartSoftwareDateTimeModelBinder(IClock clock, DateTimeModelBinder dateTimeModelBinder)
    {
        _clock = clock;
        _dateTimeModelBinder = dateTimeModelBinder;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        await _dateTimeModelBinder.BindModelAsync(bindingContext);
        if (bindingContext.Result.IsModelSet && bindingContext.Result.Model is DateTime dateTime)
        {
            bindingContext.Result = ModelBindingResult.Success(_clock.Normalize(dateTime));
        }
    }
}
