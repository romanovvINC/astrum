using Astrum.Calendar.Application.Mappings;
using Astrum.Calendar.Application.Services;
using Astrum.Calendar.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Astrum.Calendar.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(CalendarProfile).Assembly);
        });
        services.AddScoped<ICalendarEventService, CalendarEventService>();
        services.AddScoped<IAccessorService, AccessorService>();
    }
}