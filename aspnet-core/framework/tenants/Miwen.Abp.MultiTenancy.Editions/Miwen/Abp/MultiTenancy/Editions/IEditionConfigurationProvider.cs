using System;
using System.Threading.Tasks;

namespace Miwen.Abp.MultiTenancy.Editions;

public interface IEditionConfigurationProvider
{
    Task<EditionConfiguration> GetAsync(Guid? tenantId = null);
}
