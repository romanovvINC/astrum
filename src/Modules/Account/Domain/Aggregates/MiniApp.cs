using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.SharedLib.Domain.Entities;

namespace Astrum.Account.Domain.Aggregates
{
    public class MiniApp : AggregateRootBase<Guid>
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public Guid? CoverId { get; set; }
    }
}
