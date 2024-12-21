using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Miwen.Abp.OpenApi.Authorization
{
    public interface IOpenApiAuthorizationService
    {
        Task<bool> AuthorizeAsync(HttpContext httpContext);
    }
}
