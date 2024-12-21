using System.Threading.Tasks;

namespace Miwen.Abp.Idempotent;

public interface IIdempotentChecker
{
    Task<IdempotentGrantResult> IsGrantAsync(IdempotentCheckContext context);
}
