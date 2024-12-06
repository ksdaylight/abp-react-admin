using Miwen.Abp.Samples;
using Xunit;

namespace Miwen.Abp.EntityFrameworkCore.Applications;

[Collection(AbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<AbpEntityFrameworkCoreTestModule>
{

}
