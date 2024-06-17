using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Account.Application.ViewModels;
using Astrum.SharedLib.Common.CQS.Implementations;

namespace Astrum.Account.Application.Features.MiniApp.Queries
{
    public class GetMiniAppListQuery : QueryResult<List<MiniAppResponse>>
    {
    }
}
