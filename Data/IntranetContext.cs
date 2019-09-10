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
    }
}