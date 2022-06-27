using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;

namespace PostGreSqlTransaction.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(TransContext transContext) : base(transContext)
    {
    }

    public IEnumerable<Account> AccountsByOwner(Guid ownerId)
    {
        return FindByCondition(a => a.UserId.Equals(ownerId)).ToList();
    }
}