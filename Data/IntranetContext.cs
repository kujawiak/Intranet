using Microsoft.EntityFrameworkCore;

namespace Intranet.Models
{
    public class IntranetContext: DbContext
    {
        public IntranetContext (DbContextOptions<IntranetContext> options)
            : base(options)
        {
        }

        public DbSet<Intranet.Models.RepoFile> RepoFile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepoFile>()
                .HasIndex(p => new { p.Version, p.GUID })
                .IsUnique(true);
        }
    }
}