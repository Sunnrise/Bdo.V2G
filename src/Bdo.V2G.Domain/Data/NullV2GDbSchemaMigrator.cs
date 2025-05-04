using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Bdo.V2G.Data;

/* This is used if database provider does't define
 * IV2GDbSchemaMigrator implementation.
 */
public class NullV2GDbSchemaMigrator : IV2GDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
