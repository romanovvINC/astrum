using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrum.Permissions.Application.Models.ViewModels
{
    public class PermissionSectionView
    {
        public Guid Id { get; set; }
        public string TitleSection { get; set; }
        public bool Permission { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModifed { get; set; }
    }
}
