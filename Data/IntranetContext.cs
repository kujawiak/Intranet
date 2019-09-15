using Microsoft.EntityFrameworkCore;

namespace Intranet.Models
{
    public class IntranetContext: DbContext
    {
        public IntranetContext (DbContextOptions<IntranetContext> options)
            : base(options)
        {
        }

        public DbSet<RepoFile> RepoFile { get; set; }
        public DbSet<RepoDir> RepoDir { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepoFile>()
                .HasIndex(p => new { p.Version, p.GUID })
                .IsUnique(true);

            modelBuilder.Entity<RepoDir>()
                .HasMany(a => a.Files)
                .WithOne(a => a.RepoDir);

            RepoDir mainDir = new RepoDir { 
                Id = 100, 
                GUID = new System.Guid("37d3f609-8df9-455c-97cd-dbb5a3a1c833"), 
                Date = System.DateTime.Now
            };

            modelBuilder.Entity<RepoDir>().HasData(
                mainDir
            );
            
            modelBuilder.Entity<RepoFile>().HasData(
                new {
                    GUID = new System.Guid("f6410304-0547-40cb-93b4-3d7e403fc8e3"),
                    Id = 1000000,
                    Version = 0,
                    Date = System.DateTime.Now,
                    RepoDirId = 100, //mainDir,
                    Size = 0
                }
            );
        }
    }
}