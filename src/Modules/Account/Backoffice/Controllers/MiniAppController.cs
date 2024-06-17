using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Infrastructure.Shared;
using Astrum.Infrastructure.Shared.Result.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Astrum.SharedLib.Common.Results;
using Astrum.Account.Application.Features.MiniApp.Queries;
using Astrum.Account.Application.ViewModels;

namespace Astrum.Account.Backoffice.Controllers
{
    [Area("MiniApp")]
    [Route("[controller]")]
    public class MiniAppController : ApiBaseController
    {
        /// <summary>
        ///     Получить все мини-приложения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(List<MiniAppResponse>), StatusCodes.Status200OK)]
        public async Task<Result<List<MiniAppResponse>>> GetAll()
        {
            var query = new GetMiniAppListQuery();
            return await Mediator.Send(query);
        }

        /// <summary>
        ///     Получить мини-приложение по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{miniAppId}")]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(MiniAppResponse), StatusCodes.Status200OK)]
        public async Task<Result<MiniAppResponse>> GetMiniApp(Guid miniAppId)
        {
            var query = new GetMiniAppQuery(miniAppId);
            return await Mediator.Send(query);
        }
    }
}
