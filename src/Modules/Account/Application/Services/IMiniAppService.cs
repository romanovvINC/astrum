using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Account.Application.ViewModels;
using Astrum.SharedLib.Common.Results;

namespace Astrum.Account.Application.Services
{
    public interface IMiniAppService
    {
        public Task<Result<List<MiniAppResponse>>> GetAll();
        public Task<Result<MiniAppResponse>> GetById(Guid id);
        public Task<Result<MiniAppResponse>> CreateAsync(MiniAppRequest miniApp);
        public Task<Result<MiniAppResponse>> UpdateAsync(MiniAppRequest miniApp);
        public Task<Result<MiniAppResponse>> DeleteAsync(Guid id);
    }
}
