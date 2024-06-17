using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Astrum.Calendar.Domain.Specifications
{
    public class GetCalendarEventsSpec : Specification<Aggregates.Calendar>
    {
        public GetCalendarEventsSpec(DateTime start, DateTime end)
        {
            Query.Include(x => x.Events.Where(x =>
                x.Start >= start && x.Start <= end || x.End <= end && x.End >= start || 
                x.Start <= start && x.End >= end ||
                x.Yearly && x.Start.Month >= start.Month &&
                x.Start.Day >= start.Day && x.End.Month <= end.Month && x.End.Day <= end.Day));
        }
    }

    public class GetCalendarByIdSpec : Specification<Aggregates.Calendar>
    {
        public GetCalendarByIdSpec(Guid id)
        {
            Query.Where(x => x.Id == id);
        }
    }

    public class GetCalendarBySummarySpec : Specification<Aggregates.Calendar>
    {
        public GetCalendarBySummarySpec(string summary)
        {
            Query.Where(x => x.Summary == summary);
        }
    }

    public class GetEventByIdSpec : Specification<Aggregates.Event>
    {
        public GetEventByIdSpec(Guid id)
        {
            Query.Where(x => x.Id == id);
        }
    }

    public class GetCalendarByGoogleIdSpec : Specification<Aggregates.Calendar>
    {
        public GetCalendarByGoogleIdSpec(IEnumerable<string> ids)
        {
            Query.Where(x => ids.Contains(x.GoogleId));
        }
    }

    public class GetEventByGoogleIdSpec : Specification<Aggregates.Event>
    {
        public GetEventByGoogleIdSpec(IEnumerable<string> ids)
        {
            Query.Where(x => ids.Contains(x.GoogleId));
        }
    }
}
