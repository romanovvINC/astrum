using Astrum.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Astrum.Permissions.Extensions;

namespace Astrum.Permissions.Startup
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddPersistenceService(configuration);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
    }
}
