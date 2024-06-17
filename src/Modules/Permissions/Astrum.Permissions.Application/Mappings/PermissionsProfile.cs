using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Permissions.Application.Models.CreateModels;
using Astrum.Permissions.Application.Models.UpdateModels;
using Astrum.Permissions.Application.Models.ViewModels;
using Astrum.Permissions.Domain.Aggregates;
using AutoMapper;

namespace Astrum.Permissions.Application.Mappings
{
    public class PermissionsProfile : Profile
    {
        public PermissionsProfile()
        {
            CreateMap<PermissionSection, PermissionSectionCreateRequest>().ReverseMap();
            CreateMap<PermissionSection, PermissionSectionUpdateRequest>().ReverseMap();
            CreateMap<PermissionSection, PermissionSectionView>().ReverseMap();

            CreateMap<PermissionSectionView, PermissionSectionUpdateRequest>().ReverseMap();
            CreateMap<PermissionSectionView, PermissionSectionView>().ReverseMap();
        }
    }
}
