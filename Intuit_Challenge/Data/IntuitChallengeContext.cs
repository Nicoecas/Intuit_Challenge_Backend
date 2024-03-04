using Intuit_Challenge.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intuit_Challenge.Data
{
    public class IntuitChallengeContext : DbContext
    {
        public IntuitChallengeContext(DbContextOptions<IntuitChallengeContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateDate = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateDate = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync();
        }
    }


}
