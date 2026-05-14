using System.Threading.Tasks;

namespace Miwen.Abp.DataProtection;

public interface IDataAccessStrategyContributor
{
    string Name { get; }
    Task<DataAccessStrategyState> GetOrNullAsync(DataAccessStrategyContributorContext context);
}
