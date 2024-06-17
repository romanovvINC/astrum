using Astrum.Calendar.Domain.Aggregates;
using Astrum.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Astrum.Calendar;

public class CalendarDbContext : BaseDbContext
{
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Aggregates.Calendar> Calendars { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Calendar");
    }
}