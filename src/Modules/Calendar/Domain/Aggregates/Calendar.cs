using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.SharedLib.Domain.Entities;

namespace Astrum.Calendar.Domain.Aggregates
{
    public class Calendar : AggregateRootBase<Guid>
    {
        public string? GoogleId { get; set; }
        public string Summary { get; set; }
        public string BackgroundColor { get; set; }
        public List<Event>? Events { get; set; }
    }
}
