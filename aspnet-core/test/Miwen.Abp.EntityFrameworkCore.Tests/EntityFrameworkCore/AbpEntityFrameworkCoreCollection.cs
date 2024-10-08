using Xunit;

namespace Miwen.Abp.EntityFrameworkCore;

[CollectionDefinition(AbpTestConsts.CollectionDefinitionName)]
public class AbpEntityFrameworkCoreCollection : ICollectionFixture<AbpEntityFrameworkCoreFixture>
{

}
