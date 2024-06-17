using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Account.Aggregates;
using Astrum.Account.Application.Features.MiniApp;
using Astrum.Account.Application.ViewModels;
using Astrum.Account.Domain.Aggregates;
using Astrum.Account.Features.Profile;
using AutoMapper;

namespace Astrum.Account.Mappings
{
    public class MiniAppProfile : Profile
    {
        public MiniAppProfile() 
        {
            CreateMap<MiniApp, MiniAppResponse>();
            CreateMap<MiniAppRequest, MiniApp>();
        }
    }
}
