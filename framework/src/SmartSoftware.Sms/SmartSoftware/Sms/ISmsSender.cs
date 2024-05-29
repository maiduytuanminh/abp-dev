using System.Threading.Tasks;

namespace SmartSoftware.Sms;

public interface ISmsSender
{
    Task SendAsync(SmsMessage smsMessage);
}
