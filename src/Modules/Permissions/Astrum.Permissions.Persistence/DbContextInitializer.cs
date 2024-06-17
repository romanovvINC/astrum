using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Infrastructure.Services.DbInitializer;
using Microsoft.EntityFrameworkCore;

namespace Astrum.Permissions.Persistence
{
    public class DbContextInitializer : IDbContextInitializer
    {
        private readonly PermissionsDbContext _dbContext;
        public DbContextInitializer(PermissionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Migrate(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.MigrateAsync(cancellationToken);
        }

        public Task Seed(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
