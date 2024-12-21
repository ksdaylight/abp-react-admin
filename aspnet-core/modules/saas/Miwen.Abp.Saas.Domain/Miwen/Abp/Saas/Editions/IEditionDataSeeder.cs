using System.Threading.Tasks;

namespace Miwen.Abp.Saas.Editions;

public interface IEditionDataSeeder
{
    Task SeedDefaultEditionsAsync();
}
