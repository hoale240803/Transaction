using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;
using PostGreSqlTransaction.Repositories.Contracts;

namespace PostGreSqlTransaction.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransContext _context;
        public IUserRepository Users { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(TransContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public UnitOfWork() : this(new TransContext())
        {
        }

        public int Save()
        {
            return _context.SaveChanges();
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
