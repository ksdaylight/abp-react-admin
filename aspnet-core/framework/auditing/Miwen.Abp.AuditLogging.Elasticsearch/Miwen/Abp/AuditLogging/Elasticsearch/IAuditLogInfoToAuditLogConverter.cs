using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace Miwen.Abp.AuditLogging;

public interface IAuditLogInfoToAuditLogConverter
{
    Task<AuditLog> ConvertAsync(AuditLogInfo auditLogInfo);
}
