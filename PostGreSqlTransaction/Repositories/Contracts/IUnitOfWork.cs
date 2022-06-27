using PostGreSqlTransaction.Interfaces;

namespace PostGreSqlTransaction.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
    }
}
