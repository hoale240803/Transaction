using Microsoft.EntityFrameworkCore;

namespace PostGreSqlTransaction.Entities
{
    public class TransContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(@"Host=localhost;Username=user;Password=secret1234;Database=transa");
    }
}
