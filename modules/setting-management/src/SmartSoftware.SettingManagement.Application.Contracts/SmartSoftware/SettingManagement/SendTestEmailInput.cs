﻿using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.SettingManagement;

public class SendTestEmailInput
{
    [Required]
    public string SenderEmailAddress { get; set; }

    [Required]
    public string TargetEmailAddress { get; set; }

    [Required]
    public string Subject { get; set; }
    
    public string Body { get; set; }
}