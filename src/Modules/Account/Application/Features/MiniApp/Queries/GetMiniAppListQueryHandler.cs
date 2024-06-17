using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Account.Application.Repositories;
using Astrum.Account.Application.Services;
using Astrum.Account.Application.ViewModels;
using Astrum.Account.Domain.Specifications;
using Astrum.SharedLib.Common.CQS.Implementations;
using Astrum.SharedLib.Common.Results;
using AutoMapper;

namespace Astrum.Account.Application.Features.MiniApp.Queries
{
    internal class GetMiniAppListQueryHandler : QueryResultHandler<GetMiniAppListQuery, List<MiniAppResponse>>
    {
        private readonly IMiniAppService _miniAppService;

        public GetMiniAppListQueryHandler(IMiniAppService miniAppService)
        {
            _miniAppService = miniAppService;
        }

        public override async Task<Result<List<MiniAppResponse>>> Handle(GetMiniAppListQuery query, CancellationToken cancellationToken = default)
        {
            var response = await _miniAppService.GetAll();
            return response;
        }
    }
}
