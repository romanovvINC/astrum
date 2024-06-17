using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Permissions.Application.Models.CreateModels;
using Astrum.Permissions.Application.Models.UpdateModels;
using Astrum.Permissions.Application.Models.ViewModels;
using Astrum.Permissions.Domain.Aggregates;
using Astrum.Permissions.Domain.Specifications;
using Astrum.Permissions.DomainServices.Repositories;
using Astrum.SharedLib.Common.Results;
using AutoMapper;

namespace Astrum.Permissions.Application.Services
{
    public class PermissionSectionService : IPermissionSectionService
    {
        private readonly IPermissionSectionsRepository _repository;
        private readonly IMapper _mapper;

        public PermissionSectionService(IPermissionSectionsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<PermissionSectionView>>> GetPermissionsSections(CancellationToken cancellationToken = default)
        {
            var spec = new GetPermissionsSectionsSpec();
            var permissionsSections = await _repository.ListAsync(spec, cancellationToken);
            return Result.Success(_mapper.Map<List<PermissionSectionView>>(permissionsSections));
        }

        public async Task<Result<List<PermissionSectionView>>> GetPermissionsSectionsByAccess(bool permission, CancellationToken cancellationToken = default)
        {
            var spec = new GetPermissionsSectionsByAccess(permission);
            var permissionsSections = await _repository.ListAsync(spec, cancellationToken);
            return Result.Success(_mapper.Map<List<PermissionSectionView>>(permissionsSections));
        }

        public async Task<Result<PermissionSectionView>> GetPermissionSectionById(Guid id, CancellationToken cancellationToken = default)
        {
            var spec = new GetPermissionSectionById(id);
            var permissionSection = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
            if (permissionSection == null)
            {
                return Result.NotFound("Доступ раздела не найден.");
            }
            return Result.Success(_mapper.Map<PermissionSectionView>(permissionSection));
        }

        public async Task<Result<PermissionSectionView>> CreatePermissionSection(PermissionSectionCreateRequest permissionSectionCreateRequest, CancellationToken cancellationToken = default)
        {
            var permissionSection = _mapper.Map<PermissionSection>(permissionSectionCreateRequest);
            permissionSection = await _repository.AddAsync(permissionSection);
            try
            {
                await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при создании доступа раздела");
            }
            var permissionSectionView = _mapper.Map<PermissionSectionView>(permissionSection);
            return Result.Success(permissionSectionView);
        }

        public async Task<Result<PermissionSectionView>> UpdatePermissionSection(Guid id, PermissionSectionUpdateRequest permissionSectionUpdateRequest, CancellationToken cancellationToken = default)
        {
            var permissionSection = _mapper.Map<PermissionSection>(permissionSectionUpdateRequest);
            var spec = new GetPermissionSectionById(id);

            var dbPermissionSection = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
            if (dbPermissionSection == null)
            {
                return Result.NotFound("Доступ раздела не найден.");
            }
            dbPermissionSection.TitleSection = permissionSection.TitleSection;
            dbPermissionSection.Permission = permissionSection.Permission;
            try
            {
                _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при обновлении доступа раздела");
            }
            var permissionSectionView = _mapper.Map<PermissionSectionView>(permissionSection);
            return Result.Success(permissionSectionView);
        }

        public async Task<Result<PermissionSectionView>> DeletePermissionSection(Guid id, CancellationToken cancellationToken = default)
        {
            var spec = new GetPermissionSectionById(id);
            var permissionSection = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
            if (permissionSection == null)
            {
                return Result.NotFound("Доступ раздела не найден.");
            }
            try
            {
                await _repository.DeleteAsync(permissionSection, cancellationToken);
                await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при удалении доступа раздела");
            }
            var permissionSectionView = _mapper.Map<PermissionSectionView>(permissionSection);
            return Result.Success(permissionSectionView);
        }
    }
}
