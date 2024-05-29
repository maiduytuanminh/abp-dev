using SmartSoftware.Http.Modeling;

namespace SmartSoftware.Http.Client.ClientProxying;

public interface IClientProxyApiDescriptionFinder
{
    ActionApiDescriptionModel? FindAction(string methodName);

    ApplicationApiDescriptionModel GetApiDescription();
}
