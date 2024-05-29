namespace SmartSoftware.AspNetCore.Components.Web;

public class SmartSoftwareAuthenticationOptions
{
    public string LoginUrl { get; set; } = "Account/Login";

    public string LogoutUrl { get; set; } = "Account/Logout"; 
}