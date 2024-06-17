using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.SharedLib.Domain.Entities;

namespace Astrum.Calendar.Domain.Aggregates
{
    public class Event : AggregateRootBase<Guid>
    {
        public string? GoogleId { get; set; }
        public Guid CalendarId { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Yearly { get; set; }
    }
}
