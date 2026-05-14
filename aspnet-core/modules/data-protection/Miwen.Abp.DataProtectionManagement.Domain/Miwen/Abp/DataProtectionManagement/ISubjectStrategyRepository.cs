using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Miwen.Abp.DataProtectionManagement;

public interface ISubjectStrategyRepository : IBasicRepository<SubjectStrategy, Guid>
{
    Task<SubjectStrategy> FindBySubjectAsync(
        string subjectName,
        string subjectId,
        CancellationToken cancellationToken = default);
}
