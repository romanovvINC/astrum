using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Astrum.Infrastructure.Shared;
using Astrum.Logging.Services;
using Astrum.Permissions.Application.Services;
using Astrum.Permissions.Application.Models.ViewModels;
using Astrum.SharedLib.Common.Results;
using Astrum.Infrastructure.Shared.Result.AspNetCore;
using Astrum.Permissions.Application.Models.CreateModels;
using Astrum.Permissions.Application.Models.UpdateModels;
using Microsoft.AspNetCore.Authorization;

namespace Astrum.Permissions.Backoffice.Controllers
{
    [Route("[controller]")]
    public class PermissionSectionsController : ApiBaseController
    {
        private readonly IPermissionSectionService _service;
        private readonly ILogHttpService _logger;

        public PermissionSectionsController(IPermissionSectionService service, ILogHttpService logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("/api/permissions/get-permissions-sections")]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(List<PermissionSectionView>), StatusCodes.Status200OK)]
        public async Task<Result<List<PermissionSectionView>>> GetPermissionsSections()
        {
            var result = await _service.GetPermissionsSections();
            return result;
        }

        [HttpGet("/api/permissions/get-permissions-sections-by-access")]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(List<PermissionSectionView>), StatusCodes.Status200OK)]
        public async Task<Result<List<PermissionSectionView>>> GetPermissionsSections(bool permission)
        {
            var result = await _service.GetPermissionsSectionsByAccess(permission);
            return result;
        }

        [HttpGet("/api/permissions/get-permission-section-by-id")]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(PermissionSectionView), StatusCodes.Status200OK)]
        public async Task<Result<PermissionSectionView>> GetPermissionSectionById(Guid id)
        {
            var result = await _service.GetPermissionSectionById(id);
            if (!result.IsSuccess)
            {
                _logger.Log(id, result, HttpContext, "Получен доступ раздела.", Logging.Entities.TypeRequest.GET, Logging.Entities.ModuleAstrum.Permissions);
            }
            return result;
        }

        [HttpPost("/api/permissions/create-permission-section")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(PermissionSectionView), StatusCodes.Status200OK)]
        public async Task<Result<PermissionSectionView>> CreatePermissionSection([FromBody] PermissionSectionCreateRequest permissionSection)
        {
            var result = await _service.CreatePermissionSection(permissionSection);
            _logger.Log(permissionSection, result, HttpContext, "Создан раздел доступа", Logging.Entities.TypeRequest.POST, Logging.Entities.ModuleAstrum.Permissions);
            return result;
        }

        [HttpPut("/api/permissions/update-permission-section/{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(PermissionSectionView), StatusCodes.Status200OK)]
        public async Task<Result<PermissionSectionView>> UpdateSection([FromRoute]Guid id, [FromBody] PermissionSectionUpdateRequest permissionSection)
        {
            var result = await _service.UpdatePermissionSection(id, permissionSection);
            _logger.Log(permissionSection, result, HttpContext, "Обновлён раздел доступа", Logging.Entities.TypeRequest.PUT, Logging.Entities.ModuleAstrum.Permissions);
            return result;
        }

        [HttpPut("/api/permissions/delete-permission-section/{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [TranslateResultToActionResult]
        [ProducesDefaultResponseType(typeof(Result))]
        [ProducesResponseType(typeof(PermissionSectionView), StatusCodes.Status200OK)]
        public async Task<Result<PermissionSectionView>> DeleteSection([FromRoute] Guid id)
        {
            var result = await _service.DeletePermissionSection(id);
            _logger.Log(id, result, HttpContext, "Удалён раздел доступа", Logging.Entities.TypeRequest.DELETE, Logging.Entities.ModuleAstrum.Permissions);
            return result;
        }
    }
}
