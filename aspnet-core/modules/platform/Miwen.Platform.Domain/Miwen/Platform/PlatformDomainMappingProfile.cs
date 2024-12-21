using AutoMapper;
using Miwen.Platform.Layouts;
using Miwen.Platform.Menus;
using Miwen.Platform.Packages;

namespace Miwen.Platform;

public class PlatformDomainMappingProfile : Profile
{
    public PlatformDomainMappingProfile()
    {
        CreateMap<Layout, LayoutEto>();

        CreateMap<Menu, MenuEto>();
        CreateMap<UserMenu, UserMenuEto>();
        CreateMap<RoleMenu, RoleMenuEto>();

        CreateMap<Package, PackageEto>();
    }
}
