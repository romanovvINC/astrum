using Microsoft.AspNetCore.WebUtilities;

namespace FuckWeb.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private IConfiguration _configuration;

    public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var validToken = _configuration.GetSection("ApiCredentials")["Token"];
        var token = ExtractTokenFromQueryString(context.Request);
        
        if (validToken != token)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Атата так делать");
            return;
        }
        
        await _next(context);
    }
    
    private string ExtractTokenFromQueryString(HttpRequest request)
    {
        string queryString = request.QueryString.Value;
        var query = QueryHelpers.ParseQuery(queryString);
        
        if (query.ContainsKey("token"))
        {
            return query["token"];
        }
        return null;
    }
}