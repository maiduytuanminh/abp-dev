namespace SmartSoftware.Emailing;

/// <summary>
/// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
/// </summary>
public static class EmailSettingNames
{
    /// <summary>
    /// SmartSoftware.Net.Mail.DefaultFromAddress
    /// </summary>
    public const string DefaultFromAddress = "SmartSoftware.Mailing.DefaultFromAddress";

    /// <summary>
    /// SmartSoftware.Net.Mail.DefaultFromDisplayName
    /// </summary>
    public const string DefaultFromDisplayName = "SmartSoftware.Mailing.DefaultFromDisplayName";

    /// <summary>
    /// SMTP related email settings.
    /// </summary>
    public static class Smtp
    {
        /// <summary>
        /// SmartSoftware.Net.Mail.Smtp.Host
        /// </summary>
        public const string Host = "SmartSoftware.Mailing.Smtp.Host";

        /// <summary>
        /// SmartSoftware.Net.Mail.Smtp.Port
        /// </summary>
        public const string Port = "SmartSoftware.Mailing.Smtp.Port";

        /// <summary>
        /// SmartSoftware.Net.Mail.Smtp.UserName
        /// </summary>
        public const string UserName = "SmartSoftware.Mailing.Smtp.UserName";

        /// <summary>
        /// SmartSoftware.Net.Mail.Smtp.Password
        /// </summary>
        public const string Password = "SmartSoftware.Mailing.Smtp.Password";

        /// <summary>
        /// SmartSoftware.Net.Mail.Smtp.Domain
        /// </summary>
        public const string Domain = "SmartSoftware.Mailing.Smtp.Domain";

        /// <summary>
        /// SmartSoftware.Net.Mail.Smtp.EnableSsl
        /// </summary>
        public const string EnableSsl = "SmartSoftware.Mailing.Smtp.EnableSsl";

        /// <summary>
        /// SmartSoftware.Net.Mail.Smtp.UseDefaultCredentials
        /// </summary>
        public const string UseDefaultCredentials = "SmartSoftware.Mailing.Smtp.UseDefaultCredentials";
    }
}
