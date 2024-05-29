using AutoMapper;
using SmartSoftware.AutoMapper;
using SmartSoftware.Identity.Web.Pages.Identity.Roles;
using CreateUserModalModel = SmartSoftware.Identity.Web.Pages.Identity.Users.CreateModalModel;
using EditUserModalModel = SmartSoftware.Identity.Web.Pages.Identity.Users.EditModalModel;

namespace SmartSoftware.Identity.Web;

public class SmartSoftwareIdentityWebAutoMapperProfile : Profile
{
    public SmartSoftwareIdentityWebAutoMapperProfile()
    {
        CreateUserMappings();
        CreateRoleMappings();
    }

    protected virtual void CreateUserMappings()
    {
        //List
        CreateMap<IdentityUserDto, EditUserModalModel.UserInfoViewModel>()
            .Ignore(x => x.Password);

        //CreateModal
        CreateMap<CreateUserModalModel.UserInfoViewModel, IdentityUserCreateDto>()
            .MapExtraProperties()
            .ForMember(dest => dest.RoleNames, opt => opt.Ignore());

        CreateMap<IdentityRoleDto, CreateUserModalModel.AssignedRoleViewModel>()
            .ForMember(dest => dest.IsAssigned, opt => opt.Ignore());

        //EditModal
        CreateMap<EditUserModalModel.UserInfoViewModel, IdentityUserUpdateDto>()
            .MapExtraProperties()
            .ForMember(dest => dest.RoleNames, opt => opt.Ignore());

        CreateMap<IdentityRoleDto, EditUserModalModel.AssignedRoleViewModel>()
            .ForMember(dest => dest.IsAssigned, opt => opt.Ignore());

        CreateMap<IdentityUserDto, EditUserModalModel.DetailViewModel>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
    }

    protected virtual void CreateRoleMappings()
    {
        //List
        CreateMap<IdentityRoleDto, EditModalModel.RoleInfoModel>();

        //CreateModal
        CreateMap<CreateModalModel.RoleInfoModel, IdentityRoleCreateDto>()
            .MapExtraProperties();

        //EditModal
        CreateMap<EditModalModel.RoleInfoModel, IdentityRoleUpdateDto>()
            .MapExtraProperties();
    }
}
