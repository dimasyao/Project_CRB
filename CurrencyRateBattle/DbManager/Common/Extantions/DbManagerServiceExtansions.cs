using DbProvider.Data;
using DbProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DbProvider.Common.Extantions;

public static class DbManagerServiceExtansions
{
    /// <summary>
    /// Registration DBContext, DataReader, DataWriter in DI
    /// </summary>
    /// <param name="services">DI</param>
    /// <param name="connectionString">Connection to bd</param>
    /// <returns></returns>
    public static IServiceCollection AddDbManager(this IServiceCollection services, string connectionString)
    {
        _ = services.AddDbContext<CurrencyBattleDbContext>(options => options.UseNpgsql(connectionString))
            .AddScoped<IDataReader, DataReader>()
            .AddScoped<IDbWriter, DbWriter>();

        return services;
    }
}
