using PostGreSqlTransaction.Entities;

namespace PostGreSqlTransaction.Interfaces;

public interface IAccountRepository : IBaseRepository<Account>
{
    IEnumerable<Account> AccountsByOwner(Guid ownerId);
}