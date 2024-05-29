using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Auditing;

namespace SmartSoftware.Account;

public class ResetPasswordDto
{
    public Guid UserId { get; set; }

    [Required]
    public string ResetToken { get; set; }

    [Required]
    [DisableAuditing]
    public string Password { get; set; }
}
