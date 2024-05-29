using System.Net.Mail;
using System.Threading.Tasks;
using NSubstitute;
using SmartSoftware.BackgroundJobs;
using SmartSoftware.Emailing.Smtp;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.MailKit;

//Tests are commented because those tests can pass only when a true email configuration is set.
public class MailKitSmtpEmailSender_Tests : SmartSoftwareIntegratedTest<SmartSoftwareMailKitTestModule>
{
    //[Fact]
    public async Task ShouldSendMailMessageAsync()
    {
        var mailSender = CreateMailKitEmailSender();
        var mailMessage = new MailMessage("from_mail_address@asd.com", "to_mail_address@asd.com", "subject", "body")
        { IsBodyHtml = true };

        await mailSender.SendAsync(mailMessage);
    }

    //[Fact]
    public async Task ShouldSendMailMessage()
    {
        var mailSender = CreateMailKitEmailSender();
        var mailMessage = new MailMessage("from_mail_address@asd.com", "to_mail_address@asd.com", "subject", "body")
        {
            IsBodyHtml = true
        };

        await mailSender.SendAsync(mailMessage);
    }

    private static MailKitSmtpEmailSender CreateMailKitEmailSender()
    {
        var currentTenant = Substitute.For<ICurrentTenant>();
        var mailConfig = Substitute.For<ISmtpEmailSenderConfiguration>();
        var bgJob = Substitute.For<IBackgroundJobManager>();

        mailConfig.GetHostAsync().Returns(Task.FromResult("stmp_server_name"));
        mailConfig.GetUserNameAsync().Returns(Task.FromResult("mail_server_user_name"));
        mailConfig.GetPasswordAsync().Returns(Task.FromResult("mail_server_password"));
        mailConfig.GetPortAsync().Returns(Task.FromResult(587));
        mailConfig.GetEnableSslAsync().Returns(Task.FromResult(false));

        var mailSender = new MailKitSmtpEmailSender(currentTenant, mailConfig, bgJob, null);
        return mailSender;
    }
}
