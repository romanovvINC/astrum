using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Astrum.Calendar.Services;

public class AccessorService : IAccessorService
{
    private readonly IConfiguration configuration;
    private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);
    private readonly IWebHostEnvironment _hostingEnvironment;

    public AccessorService(IConfiguration config, IWebHostEnvironment hostingEnvironment)
    {
        configuration = config;
        _hostingEnvironment = hostingEnvironment;
    }

    #region IAccessorService Members

    public async Task<CalendarService> GetAccess()
    {
        GoogleCredential credential;
        var scopes = new[] {CalendarService.Scope.Calendar, CalendarService.Scope.CalendarEvents};
        var settings = configuration.GetSection("Calendar:Google-Auth")
            .Get<Dictionary<string, object>>();
        var json = JsonConvert.SerializeObject(settings);
        credential = GoogleCredential.FromJson(json).CreateScoped(scopes);
        return new CalendarService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Calendar API Integration"
        });
    }

    #endregion
}