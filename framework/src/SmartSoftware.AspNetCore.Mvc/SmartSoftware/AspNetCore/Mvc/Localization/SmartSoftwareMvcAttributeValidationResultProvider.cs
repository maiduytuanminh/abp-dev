using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.Validation;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Validation;

namespace SmartSoftware.AspNetCore.Mvc.Localization;

[Dependency(ReplaceServices = true)]
public class SmartSoftwareMvcAttributeValidationResultProvider : DefaultAttributeValidationResultProvider
{
    private readonly SmartSoftwareMvcDataAnnotationsLocalizationOptions _ssMvcDataAnnotationsLocalizationOptions;
    private readonly IStringLocalizerFactory _stringLocalizerFactory;

    public SmartSoftwareMvcAttributeValidationResultProvider(
        IOptions<SmartSoftwareMvcDataAnnotationsLocalizationOptions> ssMvcDataAnnotationsLocalizationOptions,
        IStringLocalizerFactory stringLocalizerFactory)
    {
        _ssMvcDataAnnotationsLocalizationOptions = ssMvcDataAnnotationsLocalizationOptions.Value;
        _stringLocalizerFactory = stringLocalizerFactory;
    }

    public override ValidationResult? GetOrDefault(ValidationAttribute validationAttribute, object? validatingObject, ValidationContext validationContext)
    {
        var resourceSource = _ssMvcDataAnnotationsLocalizationOptions.AssemblyResources.GetOrDefault(validationContext.ObjectType.Assembly);
        if (resourceSource == null)
        {
            return base.GetOrDefault(validationAttribute, validatingObject, validationContext);
        }

        if (validationAttribute.ErrorMessage == null)
        {
            ValidationAttributeHelper.SetDefaultErrorMessage(validationAttribute);
        }

        if (validationAttribute.ErrorMessage != null)
        {
            validationAttribute.ErrorMessage = _stringLocalizerFactory.Create(resourceSource)[validationAttribute.ErrorMessage];
        }

        return base.GetOrDefault(validationAttribute, validatingObject, validationContext);
    }
}
