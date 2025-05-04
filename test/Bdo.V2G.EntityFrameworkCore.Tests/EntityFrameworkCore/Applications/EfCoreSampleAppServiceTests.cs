using Bdo.V2G.Samples;
using Xunit;

namespace Bdo.V2G.EntityFrameworkCore.Applications;

[Collection(V2GTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<V2GEntityFrameworkCoreTestModule>
{

}
