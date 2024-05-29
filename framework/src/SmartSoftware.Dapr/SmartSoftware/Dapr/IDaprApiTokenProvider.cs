namespace SmartSoftware.Dapr;

public interface IDaprApiTokenProvider
{
    string? GetDaprApiToken();

    string? GetAppApiToken();
}
