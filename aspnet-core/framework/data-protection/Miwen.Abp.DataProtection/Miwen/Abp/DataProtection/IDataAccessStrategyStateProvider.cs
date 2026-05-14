using System.Threading.Tasks;

namespace Miwen.Abp.DataProtection;

public interface IDataAccessStrategyStateProvider
{
    Task<DataAccessStrategyState> GetOrNullAsync();
}
