using System.Threading.Tasks;

namespace Bdo.V2G.Data;

public interface IV2GDbSchemaMigrator
{
    Task MigrateAsync();
}
