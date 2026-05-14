using Miwen.Abp.DataProtection.Models;
using System.Threading.Tasks;

namespace Miwen.Abp.DataProtection;

public interface IDataAccessEntityTypeInfoProvider
{
    Task<EntityTypeInfoModel> GetEntitTypeInfoAsync(DataAccessEntitTypeInfoContext context);
}
