using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetIdentityPlayground.Infrastructure.Persistence
{
    public class ReadOnlyDbContext : IdentityDbContext
    {
        public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options)
            : base(options)
        {
        }
    }
}
