﻿using Microsoft.Extensions.DependencyInjection;

namespace Astrum.Calendar.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // var currentAssembly = typeof().Assembly;
        //
        // services.AddMediatR(currentAssembly);
        // services.AddAutoMapper(cfg =>
        // {
        //     cfg.AddMaps(currentAssembly);
        // });
    }
}