using Miwen.Abp.Samples;
using Xunit;

namespace Miwen.Abp.EntityFrameworkCore.Domains;

[Collection(AbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<AbpEntityFrameworkCoreTestModule>
{

}
