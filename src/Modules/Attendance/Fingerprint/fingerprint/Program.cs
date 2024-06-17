using FuckWeb.Common.Interfaces;
using FuckWeb.Common.Services;
using FuckWeb.Data;
using FuckWeb.Features.FingerPrint;
using FuckWeb.Features.WorkingSession;
using FuckWeb.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FDbContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<FingerCheckService>();
builder.Services.AddScoped<WorkingSessionService>();
builder.Services.AddScoped<ITimeZoneService,TimeZoneService>();
builder.Services.AddScoped<IDateTimeService,DateTimeService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("AllowYouba", builder =>
{
    builder.WithOrigins("https://localhost:3000", "https://git.66bit.ru")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowYouba");
app.UseRouting();

app.UseAuthorization();

app.UseWhen(x => x.Request.Path.StartsWithSegments("/fcheck", StringComparison.OrdinalIgnoreCase),
    builder => builder.UseMiddleware<AuthMiddleware>());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();