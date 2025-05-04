using Volo.Abp.Modularity;

namespace Bdo.V2G;

/* Inherit from this class for your domain layer tests. */
public abstract class V2GDomainTestBase<TStartupModule> : V2GTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
