using Microsoft.EntityFrameworkCore;
using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;

namespace PostGreSqlTransaction.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(TransContext transContext) : base(transContext)
    {
    }

    public async Task<IEnumerable<Account>> AccountsByUser(Guid ownerId)
    {
        return await FindByCondition(a => a.UserId.Equals(ownerId)).ToListAsync();
    }
}