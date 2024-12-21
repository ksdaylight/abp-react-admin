using Miwen.Platform.Datas;
using Miwen.Platform.Layouts;
using Miwen.Platform.Menus;
using Miwen.Platform.Packages;
using Miwen.Platform.Portal;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Miwen.Platform.EntityFrameworkCore;

[ConnectionStringName(PlatformDbProperties.ConnectionStringName)]
public interface IPlatformDbContext : IEfCoreDbContext
{
    DbSet<Menu> Menus { get; }
    DbSet<Layout> Layouts { get; }
    DbSet<RoleMenu> RoleMenus { get; }
    DbSet<UserMenu> UserMenus { get; }
    DbSet<UserFavoriteMenu> UserFavoriteMenus { get; }
    DbSet<Data> Datas { get; }
    DbSet<DataItem> DataItems { get; }
    DbSet<Package> Packages { get; }
    DbSet<PackageBlob> PackageBlobs { get; }
    DbSet<Enterprise> Enterprises { get; }
}
