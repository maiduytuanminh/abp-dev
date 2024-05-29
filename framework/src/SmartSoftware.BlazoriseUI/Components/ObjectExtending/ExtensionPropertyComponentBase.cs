﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Blazorise;
using Blazorise.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.Data;
using SmartSoftware.Localization;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.BlazoriseUI.Components.ObjectExtending;

public abstract class ExtensionPropertyComponentBase<TEntity, TResourceType> : OwningComponentBase
    where TEntity : IHasExtraProperties
{
    [Inject]
    public IStringLocalizerFactory StringLocalizerFactory { get; set; } = default!;

    [Inject]
    public ISmartSoftwareEnumLocalizer SmartSoftwareEnumLocalizer { get; set; } = default!;

    [Inject]
    public IValidationMessageLocalizerAttributeFinder ValidationMessageLocalizerAttributeFinder { get; set; } = default!;

    [Parameter]
    public TEntity Entity { get; set; } = default!;

    [Parameter]
    public ObjectExtensionPropertyInfo PropertyInfo { get; set; } = default!;

    [Parameter]
    public SmartSoftwareBlazorMessageLocalizerHelper<TResourceType> LH { get; set; } = default!;

    [Parameter]
    public ExtensionPropertyModalType? ModalType { get; set; }

    protected virtual void Validate(ValidatorEventArgs e)
    {
        e.Status = ValidationStatus.Success;

        var validationAttributes = PropertyInfo.GetValidationAttributes();
        var validationContext = new ValidationContext(Entity)
        {
            DisplayName = PropertyInfo.Name,
            MemberName = PropertyInfo.Name
        };

        foreach (var validationAttribute in validationAttributes)
        {
            var result = validationAttribute.GetValidationResult(e.Value, validationContext);
            if (result == ValidationResult.Success || result == null)
            {
                continue;
            }

            var errorMessage = result.ErrorMessage;
            if (LH != null)
            {
                var formattedErrorMessage = GetDefaultErrorMessage(validationAttribute);
                var errorMessageString = ValidationAttributeHelper.RevertErrorMessagePlaceholders(formattedErrorMessage);
                var errorMessageArguments = ValidationMessageLocalizerAttributeFinder.FindAll(errorMessage, errorMessageString)
                    ?.OrderBy(x => x.Index)
                    ?.Select(x => x.Argument);

                errorMessage = LH.Localize(errorMessageString, errorMessageArguments);
            }

            e.MemberNames = result.MemberNames;
            e.Status = ValidationStatus.Error; 
            e.ErrorText = errorMessage;
            break;
        }
    }

    protected bool IsReadonlyField => ModalType is ExtensionPropertyModalType.EditModal && PropertyInfo.UI.EditModal.IsReadOnly;
    
    private static string GetDefaultErrorMessage(ValidationAttribute validationAttribute)
    {
        if (validationAttribute is StringLengthAttribute stringLengthAttribute && stringLengthAttribute.MinimumLength != 0)
        {
            var nullable = ValidationAttributeHelper.ValidationAttributeCustomErrorMessageSetProperty.GetValue((object) validationAttribute) as bool?;
            var flag = true;
            if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
            {
                return ValidationAttributeHelper.SetErrorMessagePlaceholders("The field {0} must be a string with a minimum length of {2} and a maximum length of {1}.");
            }
        }
        return ValidationAttributeHelper.SetErrorMessagePlaceholders(ValidationAttributeHelper.ValidationAttributeErrorMessageStringProperty.GetValue((object) validationAttribute) as string);
    }
}
