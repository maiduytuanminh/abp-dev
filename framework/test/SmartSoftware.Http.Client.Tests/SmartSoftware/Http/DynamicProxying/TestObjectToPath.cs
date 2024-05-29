using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client.ClientProxying;
using SmartSoftware.Http.Modeling;

namespace SmartSoftware.Http.DynamicProxying;

public class TestObjectToPath : IObjectToPath<int>, ITransientDependency
{
    public Task<string> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, int value)
    {
        if (actionApiDescription.Name == nameof(IRegularTestController.GetObjectandCountAsync))
        {
            if (value <= 0)
            {
                value = 888;
            }
            return Task.FromResult(value.ToString());
        }

        return Task.FromResult<string>(null);
    }
}
