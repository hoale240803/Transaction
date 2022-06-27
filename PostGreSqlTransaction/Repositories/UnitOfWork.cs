using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;
using PostGreSqlTransaction.Repositories.Contracts;

namespace PostGreSqlTransaction.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransContext _context;
        public IUserRepository Users { get; private set; }
        public IAccountRepository Accounts { get; private set; }

        private UnitOfWork(TransContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Accounts = new AccountRepository(_context);
        }

        public UnitOfWork() : this(new TransContext())
        {
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IDbTransaction BeginTransaction()
        {
            return new EntityDbTransaction(_context);
        }
    }
}