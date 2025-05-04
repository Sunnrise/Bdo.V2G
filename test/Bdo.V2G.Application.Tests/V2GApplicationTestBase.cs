using Volo.Abp.Modularity;

namespace Bdo.V2G;

public abstract class V2GApplicationTestBase<TStartupModule> : V2GTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
