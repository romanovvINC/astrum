using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Account.Application.Repositories;
using Astrum.Account.Application.ViewModels;
using Astrum.Account.Domain.Aggregates;
using Astrum.Account.Domain.Specifications;
using Astrum.SharedLib.Common.Results;
using Astrum.Storage.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Astrum.Account.Application.Services
{
    public class MiniAppService : IMiniAppService
    {
        private readonly IMiniAppRepository _miniAppRepository;
        private readonly IFileStorage _fileStorage;
        private readonly IMapper _mapper;
        private readonly ILogger<IMiniAppService> _logger;

        public MiniAppService(IMiniAppRepository miniappRepository, IFileStorage fileStorage, IMapper mapper, ILogger<IMiniAppService> logger)
        {
            _miniAppRepository = miniappRepository;
            _fileStorage = fileStorage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<List<MiniAppResponse>>> GetAll()
        {
            var spec = new GetMiniAppsSpec();
            var miniapps = await _miniAppRepository.ListAsync(spec);

            var response = _mapper.Map<List<MiniAppResponse>>(miniapps);
            response.ForEach(async miniapp =>
            {
                var imageId = miniapps.FirstOrDefault(m => m.Id == miniapp.Id)?.CoverId;
                if (imageId.HasValue)
                    miniapp.CoverUrl = await _fileStorage.GetFileUrl(imageId.Value);
            });

            return Result.Success(response);
        }

        public async Task<Result<MiniAppResponse>> GetById(Guid id)
        {
            var spec = new GetMiniAppByIdSpec(id);
            var miniapp = await _miniAppRepository.FirstOrDefaultAsync(spec);

            if (miniapp == null)
            {
                return Result.NotFound("Мини-приложение не найдено.");
            }

            var response = _mapper.Map<MiniAppResponse>(miniapp);
            if (miniapp.CoverId.HasValue)
            {
                response.CoverUrl = await _fileStorage.GetFileUrl(miniapp.CoverId.Value);
            }

            return Result.Success(response);
        }

        public async Task<Result<MiniAppResponse>> CreateAsync(MiniAppRequest miniappRequest)
        {
            var miniapp = new MiniApp
            {
                Name = miniappRequest.Name,
                Link = miniappRequest.Link
            };

            if (miniappRequest.Image != null)
            {
                var uploadResult = await _fileStorage.UploadFile(miniappRequest.Image);
                if (uploadResult.Success)
                {
                    miniapp.CoverId = uploadResult.UploadedFileId;
                }
                else
                {
                    _logger.LogError("Не удалось загрузить изображение.");
                }
            }

            await _miniAppRepository.AddAsync(miniapp);
            try
            {
                await _miniAppRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при создании мини-приложения.");
            }

            var response = _mapper.Map<MiniAppResponse>(miniapp);
            if (miniapp.CoverId.HasValue)
            {
                response.CoverUrl = await _fileStorage.GetFileUrl(miniapp.CoverId.Value);
            }

            return Result.Success(response);
        }

        public async Task<Result<MiniAppResponse>> UpdateAsync(MiniAppRequest miniappRequest)
        {
            var spec = new GetMiniAppByIdSpec(miniappRequest.Id);

            var miniapp = await _miniAppRepository.FirstOrDefaultAsync(spec);
            if (miniapp == null)
                return Result.NotFound("Мини-приложение не найдено.");

            miniapp.Name = miniappRequest.Name;
            miniapp.Link = miniappRequest.Link;

            if (miniappRequest.Image != null)
            {
                var uploadResult = await _fileStorage.UploadFile(miniappRequest.Image);
                if (uploadResult.Success)
                {
                    miniapp.CoverId = uploadResult.UploadedFileId;
                }
                else
                {
                    _logger.LogError("Не удалось загрузить изображение.");
                }
            }

            await _miniAppRepository.UpdateAsync(miniapp);
            try
            {
                await _miniAppRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при обновлении мини-приложения.");
            }

            var response = _mapper.Map<MiniAppResponse>(miniapp);
            if (miniapp.CoverId.HasValue)
            {
                response.CoverUrl = await _fileStorage.GetFileUrl(miniapp.CoverId.Value);
            }

            return Result.Success(response);
        }

        public async Task<Result<MiniAppResponse>> DeleteAsync(Guid id)
        {
            var spec = new GetMiniAppByIdSpec(id);

            var miniapp = await _miniAppRepository.FirstOrDefaultAsync(spec);
            if (miniapp == null)
                return Result.NotFound("Мини-приложение не найдено.");

            try
            {
                await _miniAppRepository.DeleteAsync(miniapp);
                await _miniAppRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message, "Ошибка при удалении мини-приложения.");
            }

            var response = _mapper.Map<MiniAppResponse>(miniapp);
            if (miniapp.CoverId.HasValue)
            {
                response.CoverUrl = await _fileStorage.GetFileUrl(miniapp.CoverId.Value);
            }

            return Result.Success(response);
        }
    }
}
