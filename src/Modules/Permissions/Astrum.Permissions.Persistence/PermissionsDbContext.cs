using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Permissions.Domain.Aggregates;
using Astrum.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Astrum.Permissions.Persistence
{
    public class PermissionsDbContext : BaseDbContext
    {
        //To add migration with pmc:
        //Add-Migration Migration -Context PermissionsDbContext
        //To update db:
        //Update-Database -Context PermissionsDbContext

        public DbSet<PermissionSection> PermissionSections { get; set; }
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Permissions");
        }
    }
}
