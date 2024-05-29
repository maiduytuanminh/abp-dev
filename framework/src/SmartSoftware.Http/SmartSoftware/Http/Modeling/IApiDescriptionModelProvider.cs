namespace SmartSoftware.Http.Modeling;

public interface IApiDescriptionModelProvider
{
    ApplicationApiDescriptionModel CreateApiModel(ApplicationApiDescriptionModelRequestDto input);
}
