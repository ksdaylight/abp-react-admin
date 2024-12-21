using System.Threading.Tasks;

namespace Miwen.Abp.OssManagement;

public interface IOssObjectProcesserContributor
{
    Task ProcessAsync(OssObjectProcesserContext context);
}
