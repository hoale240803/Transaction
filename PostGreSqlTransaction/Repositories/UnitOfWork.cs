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

        public UnitOfWork(TransContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Accounts = new AccountRepository(_context);
        }

        public UnitOfWork(TransContext context,IUserRepository users, IAccountRepository accounts = null)
        {
            _context = context;
            Users = users;
            Accounts = accounts;
        }
        
        public bool Save()
        {
            var result = _context.SaveChanges() > 0;

            return result;
        }

        public async Task<bool> SaveAsync()
        {
            var result =await _context.SaveChangesAsync() > 0;

            return result;
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