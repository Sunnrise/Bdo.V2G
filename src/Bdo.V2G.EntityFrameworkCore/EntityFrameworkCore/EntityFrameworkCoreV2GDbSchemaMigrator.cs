using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bdo.V2G.Data;
using Volo.Abp.DependencyInjection;

namespace Bdo.V2G.EntityFrameworkCore;

public class EntityFrameworkCoreV2GDbSchemaMigrator
    : IV2GDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreV2GDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the V2GDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<V2GDbContext>()
            .Database
            .MigrateAsync();
    }
}
