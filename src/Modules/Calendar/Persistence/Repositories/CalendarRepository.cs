using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Astrum.Calendar.Domain.Repositories;
using Astrum.SharedLib.Persistence.Repositories;

namespace Astrum.Calendar.Persistence.Repositories
{
    public class CalendarRepository<TEntity> : EFRepository<TEntity, Guid, CalendarDbContext>, 
        ICalendarRepository<TEntity> where TEntity : class, SharedLib.Domain.Interfaces.IEntity<Guid>
    {
        public CalendarRepository(CalendarDbContext context, ISpecificationEvaluator? specificationEvaluator = null) : base(context, specificationEvaluator)
        {
        }
    }
}
