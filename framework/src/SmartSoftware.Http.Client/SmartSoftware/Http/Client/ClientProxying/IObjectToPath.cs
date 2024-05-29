using System.Threading.Tasks;
using SmartSoftware.Http.Modeling;

namespace SmartSoftware.Http.Client.ClientProxying
{
    public interface IObjectToPath<in TValue>
    {
        Task<string> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, TValue value);
    }
}
