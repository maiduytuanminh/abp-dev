using System.Threading.Tasks;
using MailKit.Net.Smtp;
using SmartSoftware.Emailing;

namespace SmartSoftware.MailKit;

public interface IMailKitSmtpEmailSender : IEmailSender
{
    Task<SmtpClient> BuildClientAsync();
}
