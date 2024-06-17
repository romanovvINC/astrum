using Astrum.Calendar.Domain.Repositories;
using Astrum.Calendar.Persistence.Repositories;
using Astrum.Infrastructure.Services.DbInitializer;
using Astrum.SharedLib.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Astrum.Calendar.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg =>
        {
            // cfg.AddMaps(typeof(AppealProfile));
        });

        services.AddCustomDbContext<CalendarDbContext>(configuration.GetConnectionString("BaseConnection"));
        services
            .AddScoped<ICalendarRepository<Domain.Aggregates.Calendar>,
                CalendarRepository<Domain.Aggregates.Calendar>>();
        services
            .AddScoped<ICalendarRepository<Domain.Aggregates.Event>,
                CalendarRepository<Domain.Aggregates.Event>>();
        services.AddScoped<IDbContextInitializer, DbContextContextInitializer>();
    }
}