using Elsa.Services;
using Volo.Abp.Guids;

namespace Miwen.Abp.Elsa;

public class AbpElsaIdGenerator : IIdGenerator
{
    private readonly IGuidGenerator _guidGenerator;

    public AbpElsaIdGenerator(IGuidGenerator guidGenerator)
    {
        _guidGenerator = guidGenerator;
    }

    public string Generate()
    {
        return _guidGenerator.Create().ToString("N");
    }
}
