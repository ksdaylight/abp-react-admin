using AutoMapper;
using Miwen.Abp.Auditing.AuditLogs;
using Miwen.Abp.Auditing.Logging;
using Miwen.Abp.Auditing.SecurityLogs;
using Miwen.Abp.AuditLogging;
using Miwen.Abp.Logging;

namespace Miwen.Abp.Auditing;

public class AbpAuditingMapperProfile : Profile
{
    public AbpAuditingMapperProfile()
    {
        CreateMap<AuditLogAction, AuditLogActionDto>()
            .MapExtraProperties();
        CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();
        CreateMap<EntityChangeWithUsername, EntityChangeWithUsernameDto>();
        CreateMap<EntityChange, EntityChangeDto>()
            .MapExtraProperties();
        CreateMap<AuditLog, AuditLogDto>()
            .MapExtraProperties();

        CreateMap<SecurityLog, SecurityLogDto>()
            .MapExtraProperties();

        CreateMap<LogField, LogFieldDto>();
        CreateMap<LogException, LogExceptionDto>();
        CreateMap<LogInfo, LogDto>();
    }
}
