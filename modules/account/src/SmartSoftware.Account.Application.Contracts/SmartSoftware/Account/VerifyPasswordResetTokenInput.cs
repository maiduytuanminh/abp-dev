using System;
using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.Account;

public class VerifyPasswordResetTokenInput
{
    public Guid UserId { get; set; }

    [Required]
    public string ResetToken { get; set; }
}
