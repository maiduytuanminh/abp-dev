using System.IO;

namespace SmartSoftware.Swashbuckle;

public interface ISwaggerHtmlResolver
{
    Stream Resolver();
}
