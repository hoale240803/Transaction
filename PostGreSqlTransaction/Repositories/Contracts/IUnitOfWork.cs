using PostGreSqlTransaction.Interfaces;

namespace PostGreSqlTransaction.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();
        IUserRepository Users { get; }
        IAccountRepository Accounts { get; }
        Task<bool> SaveAsync();
    }
}