using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrum.Permissions.Application.Models.CreateModels
{
    public class PermissionSectionCreateRequest
    {
        public string TitleSection { get; set; }
        public bool Permission { get; set; } = true;
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModifed { get; set; }
    }
}
