using System.Threading.Tasks;

namespace Miwen.Abp.AuditLogging.Elasticsearch;

public interface IIndexInitializer
{
    Task InitializeAsync();
}
