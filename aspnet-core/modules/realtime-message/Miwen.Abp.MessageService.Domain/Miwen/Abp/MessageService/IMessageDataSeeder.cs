using System;
using System.Threading.Tasks;

namespace Miwen.Abp.MessageService;

public interface IMessageDataSeeder
{
    Task SeedAsync(Guid? tenantId = null);
}
