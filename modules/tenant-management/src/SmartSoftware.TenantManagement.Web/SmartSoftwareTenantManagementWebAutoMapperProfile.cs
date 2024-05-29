using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.TenantManagement.Web.Pages.TenantManagement.Tenants;

namespace SmartSoftware.TenantManagement.Web;

public class SmartSoftwareTenantManagementWebAutoMapperProfile : Profile
{
    public SmartSoftwareTenantManagementWebAutoMapperProfile()
    {
        //List
        CreateMap<TenantDto, EditModalModel.TenantInfoModel>();

        //CreateModal
        CreateMap<CreateModalModel.TenantInfoModel, TenantCreateDto>()
            .MapExtraProperties();

        //EditModal
        CreateMap<EditModalModel.TenantInfoModel, TenantUpdateDto>()
            .MapExtraProperties();
    }
}
