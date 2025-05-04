using Bdo.V2G.Samples;
using Xunit;

namespace Bdo.V2G.EntityFrameworkCore.Domains;

[Collection(V2GTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<V2GEntityFrameworkCoreTestModule>
{

}
