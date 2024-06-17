using Microsoft.AspNetCore.Mvc;

namespace Astrum.Calendar.ViewModels;

public class EventForm
{
    public Guid Id { get; set; }
    [FromRoute]
    public string? EventId { get; set; }
    public Guid CalendarId { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public bool Yearly { get; set; }
}