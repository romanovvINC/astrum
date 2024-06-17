using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Astrum.Account.Application.Repositories;
using Astrum.Account.Domain.Aggregates;
using Astrum.SharedLib.Persistence.Repositories;

namespace Astrum.Account.Persistence.Repositories
{
    public class MiniAppRepository : EFRepository<MiniApp, Guid, AccountDbContext>,
    IMiniAppRepository
    {
        public MiniAppRepository(AccountDbContext context, ISpecificationEvaluator? specificationEvaluator = null) :
            base(context, specificationEvaluator)
        {
        }
    }
}
