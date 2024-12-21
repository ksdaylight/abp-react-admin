using Miwen.Abp.DataProtection;
using System;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.DataProtectionManagement;
public abstract class EntityRuleDtoBase : AuditedEntityDto<Guid>
{
    public Guid? TenantId { get; set; }
    public bool IsEnabled { get; set; }
    public DataAccessOperation Operation { get; set; }
    public DataAccessFilterGroup FilterGroup { get; set; }
    public Guid EntityTypeId { get; set; }
    public string EntityTypeFullName { get; set; }
    public string[] AllowProperties { get; set; }
}
