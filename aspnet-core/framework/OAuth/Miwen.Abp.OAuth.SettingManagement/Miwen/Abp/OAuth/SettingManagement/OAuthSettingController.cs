using Miwen.Abp.SettingManagement;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Miwen.Abp.OAuth.SettingManagement;

[RemoteService(Name = AbpSettingManagementRemoteServiceConsts.RemoteServiceName)]
[Area("settingManagement")]
[Route("api/setting-management/oauth")]
public class OAuthSettingController : AbpControllerBase, IOAuthSettingAppService
{
    protected IOAuthSettingAppService Service { get; }

    public OAuthSettingController(IOAuthSettingAppService service)
    {
        Service = service;
    }

    [HttpGet]
    [Route("by-current-tenant")]
    public Task<SettingGroupResult> GetAllForCurrentTenantAsync()
    {
        return Service.GetAllForCurrentTenantAsync();
    }

    [HttpGet]
    [Route("by-global")]
    public Task<SettingGroupResult> GetAllForGlobalAsync()
    {
        return Service.GetAllForGlobalAsync();
    }
}
