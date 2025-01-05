using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetIdentityPlayground.Infrastructure.Persistence
{
    public class TransactionalDbContext : IdentityDbContext
    {
        public TransactionalDbContext(DbContextOptions<TransactionalDbContext> options)
           : base(options)
        {
        }
    }
}
