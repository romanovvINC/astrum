using Microsoft.AspNetCore.Mvc;

namespace Astrum.Calendar.ViewModels;

public class CalendarForm
{
    public Guid? Id { get; set; }
    [FromRoute]
    public string Summary { get; set; }
    public string? BackgroundColor { get; set; }
    public List<EventForm>? Events { get; set; }
}