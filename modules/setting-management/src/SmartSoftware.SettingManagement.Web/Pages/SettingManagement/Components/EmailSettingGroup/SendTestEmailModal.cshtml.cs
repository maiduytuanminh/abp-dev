﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.SettingManagement.Localization;

namespace SmartSoftware.SettingManagement.Web.Pages.SettingManagement.Components.EmailSettingGroup;

[Authorize(SettingManagementPermissions.EmailingTest)]
public class SendTestEmailModal : SmartSoftwarePageModel
{
    [BindProperty]
    public SendTestEmailViewModel Input { get; set; }
    
    protected IEmailSettingsAppService EmailSettingsAppService { get; }
    
    public SendTestEmailModal(IEmailSettingsAppService emailSettingsAppService)
    {
        LocalizationResourceType = typeof(SmartSoftwareSettingManagementResource);
        EmailSettingsAppService = emailSettingsAppService;
    }

    public async Task OnGetAsync()
    {
        var emailSettings = await EmailSettingsAppService.GetAsync();
        Input = new SendTestEmailViewModel 
        {
            SenderEmailAddress = emailSettings.DefaultFromAddress,
            TargetEmailAddress = CurrentUser.Email,
            Subject = L["TestEmailSubject", new Random().Next(1000, 9999)],
            Body = L["TestEmailBody"]
        };
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();
        
        await EmailSettingsAppService.SendTestEmailAsync(ObjectMapper.Map<SendTestEmailViewModel, SendTestEmailInput>(Input));
        
        return NoContent();
    }
    
    public class SendTestEmailViewModel
    {
        [Required]
        public string SenderEmailAddress { get; set; }

        [Required]
        public string TargetEmailAddress { get; set; }

        [Required]
        public string Subject { get; set; }
        
        public string Body { get; set; }
    }
}