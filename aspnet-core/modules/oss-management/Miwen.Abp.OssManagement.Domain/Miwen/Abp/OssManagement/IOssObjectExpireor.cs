using System.Threading.Tasks;

namespace Miwen.Abp.OssManagement;
public interface IOssObjectExpireor
{
    Task ExpireAsync(ExprieOssObjectRequest request);
}
