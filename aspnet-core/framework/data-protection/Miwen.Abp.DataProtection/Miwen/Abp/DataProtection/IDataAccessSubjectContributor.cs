using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miwen.Abp.DataProtection;
public interface IDataAccessSubjectContributor
{
    string Name { get; }
    Task<List<DataAccessFilterGroup>> GetFilterGroups(DataAccessSubjectContributorContext context);
    Task<List<string>> GetAccessdProperties(DataAccessSubjectContributorContext context);
}

