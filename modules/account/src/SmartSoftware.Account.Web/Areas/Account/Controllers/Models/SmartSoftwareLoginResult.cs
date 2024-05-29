namespace SmartSoftware.Account.Web.Areas.Account.Controllers.Models;

public class SmartSoftwareLoginResult
{
    public SmartSoftwareLoginResult(LoginResultType result)
    {
        Result = result;
    }

    public LoginResultType Result { get; }

    public string Description => Result.ToString();
}
