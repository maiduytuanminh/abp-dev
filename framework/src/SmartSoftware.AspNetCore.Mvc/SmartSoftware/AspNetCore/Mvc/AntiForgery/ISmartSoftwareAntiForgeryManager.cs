namespace SmartSoftware.AspNetCore.Mvc.AntiForgery;

public interface ISmartSoftwareAntiForgeryManager
{
    void SetCookie();

    string GenerateToken();
}
