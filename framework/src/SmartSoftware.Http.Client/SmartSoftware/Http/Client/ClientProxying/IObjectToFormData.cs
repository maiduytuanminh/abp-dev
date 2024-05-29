using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SmartSoftware.Http.Modeling;

namespace SmartSoftware.Http.Client.ClientProxying;

public interface IObjectToFormData<in TValue>
{
    Task<List<KeyValuePair<string, HttpContent>>> ConvertAsync(ActionApiDescriptionModel actionApiDescription, ParameterApiDescriptionModel parameterApiDescription, TValue value);
}
