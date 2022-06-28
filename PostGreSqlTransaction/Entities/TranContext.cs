using Microsoft.EntityFrameworkCore;

namespace PostGreSqlTransaction.Entities
{
    public class TransContext : DbContext
    {
        public TransContext(DbContextOptions<TransContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}