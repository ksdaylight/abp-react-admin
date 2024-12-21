using System.Linq.Expressions;

namespace Miwen.Abp.DataProtection;
public interface IDataAccessKeywordContributor
{
    string Keyword { get; }
    Expression Contribute(DataAccessKeywordContributorContext context);
}
