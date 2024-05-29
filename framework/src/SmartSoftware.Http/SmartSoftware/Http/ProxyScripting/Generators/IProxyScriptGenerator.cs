using SmartSoftware.Http.Modeling;

namespace SmartSoftware.Http.ProxyScripting.Generators;

public interface IProxyScriptGenerator
{
    string CreateScript(ApplicationApiDescriptionModel model);
}
