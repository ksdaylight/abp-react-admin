using AutoMapper;

namespace Miwen.Abp.AuditLogging.EntityFrameworkCore;

public class AbpAuditingMapperProfile : Profile
{
    public AbpAuditingMapperProfile()
    {
        CreateMap<Volo.Abp.AuditLogging.AuditLogAction, Miwen.Abp.AuditLogging.AuditLogAction>()
            .MapExtraProperties();
        CreateMap<Volo.Abp.AuditLogging.EntityPropertyChange, Miwen.Abp.AuditLogging.EntityPropertyChange>();
        CreateMap<Volo.Abp.AuditLogging.EntityChange, Miwen.Abp.AuditLogging.EntityChange>()
            .MapExtraProperties();
        CreateMap<Volo.Abp.AuditLogging.AuditLog, Miwen.Abp.AuditLogging.AuditLog>()
            .MapExtraProperties();

        CreateMap<Volo.Abp.Identity.IdentitySecurityLog, Miwen.Abp.AuditLogging.SecurityLog>()
            .MapExtraProperties();
    }
}
