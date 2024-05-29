namespace SmartSoftware.ApiVersioning;

public interface IRequestedApiVersion
{
    string? Current { get; }
}
