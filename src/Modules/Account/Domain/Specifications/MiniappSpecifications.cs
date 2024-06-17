using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Astrum.Account.Domain.Specifications
{
    internal class MiniappSpecifications
    {
    }

    public class GetMiniAppsSpec : Specification<Aggregates.MiniApp>
    {
        public GetMiniAppsSpec()
        {
            Query
                .OrderBy(category => category.Name);
        }
    }

    public class GetMiniAppByIdSpec : Specification<Aggregates.MiniApp>
    {
        public GetMiniAppByIdSpec(Guid miniAppId)
        {
            Query
                .Where(miniApp => miniApp.Id == miniAppId);
        }
    }
}
