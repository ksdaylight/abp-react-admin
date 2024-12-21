using Volo.Abp;

namespace Miwen.Abp.DataProtection;
public class AbpDataAccessDeniedException : BusinessException
{
    public AbpDataAccessDeniedException()
    {
    }

    public AbpDataAccessDeniedException(string message)
        : base("DataProtection:010001", message)
    {
    }
}
