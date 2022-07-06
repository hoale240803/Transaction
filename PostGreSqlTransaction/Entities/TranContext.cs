using Microsoft.EntityFrameworkCore;

namespace PostGreSqlTransaction.Entities
{
    public class TransContext : DbContext
    {
        // public TransContext(DbContextOptions<TransContext> options) : base(options)
        // {
        // }

        // public TransContext(DbContextOptions options) :base(options)
        // {
        // }
        //private const string _connectionString = @"Host=localhost;Database=transa;Username=user;Password=secret1234";
        private const string _connectionString = @"Host=localhost;Database=transa;Username=user;Password=secret1234";
        

        protected override void OnConfiguring(DbContextOptionsBuilder  optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}