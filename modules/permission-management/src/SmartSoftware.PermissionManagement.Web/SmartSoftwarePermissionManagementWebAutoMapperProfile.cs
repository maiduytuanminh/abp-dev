using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.PermissionManagement.Web.Pages.SmartSoftwarePermissionManagement;

namespace SmartSoftware.PermissionManagement.Web;

public class SmartSoftwarePermissionManagementWebAutoMapperProfile : Profile
{
    public SmartSoftwarePermissionManagementWebAutoMapperProfile()
    {
        CreateMap<PermissionGroupDto, PermissionManagementModal.PermissionGroupViewModel>().Ignore(p => p.IsAllPermissionsGranted);

        CreateMap<PermissionGrantInfoDto, PermissionManagementModal.PermissionGrantInfoViewModel>()
            .ForMember(p => p.Depth, opts => opts.Ignore());

        CreateMap<ProviderInfoDto, PermissionManagementModal.ProviderInfoViewModel>();
    }
}
