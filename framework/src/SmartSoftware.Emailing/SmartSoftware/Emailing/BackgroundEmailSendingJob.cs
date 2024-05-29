using System;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.BackgroundJobs;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Emailing;

public class BackgroundEmailSendingJob : AsyncBackgroundJob<BackgroundEmailSendingJobArgs>, ITransientDependency
{
    protected IEmailSender EmailSender { get; }

    public BackgroundEmailSendingJob(IEmailSender emailSender)
    {
        EmailSender = emailSender;
    }

    public async override Task ExecuteAsync(BackgroundEmailSendingJobArgs args)
    {
        if (args.From.IsNullOrWhiteSpace())
        {
            await EmailSender.SendAsync(args.To, args.Subject, args.Body, args.IsBodyHtml, args.AdditionalEmailSendingArgs);
        }
        else
        {
            await EmailSender.SendAsync(args.From!, args.To, args.Subject, args.Body, args.IsBodyHtml, args.AdditionalEmailSendingArgs);
        }
    }
}
