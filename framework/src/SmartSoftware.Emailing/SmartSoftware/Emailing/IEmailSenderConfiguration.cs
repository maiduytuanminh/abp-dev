﻿using System.Threading.Tasks;

namespace SmartSoftware.Emailing;

/// <summary>
/// Defines configurations used while sending emails.
/// </summary>
public interface IEmailSenderConfiguration
{
    Task<string> GetDefaultFromAddressAsync();

    Task<string> GetDefaultFromDisplayNameAsync();
}
