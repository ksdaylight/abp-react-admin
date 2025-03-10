using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Miwen.Abp.OAuth.Github;

public class AbpOAuthGithubOptionsFactory : ITransientDependency
{
    protected IOptions<AbpOAuthGithubOptions> Options { get; }

    public AbpOAuthGithubOptionsFactory(
        IOptions<AbpOAuthGithubOptions> options)
    {
        Options = options;
    }

    public async virtual Task<AbpOAuthGithubOptions> CreateAsync()
    {
        await Options.SetAsync();

        return Options.Value;
    }
}
