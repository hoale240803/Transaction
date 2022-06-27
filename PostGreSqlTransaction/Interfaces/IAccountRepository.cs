using PostGreSqlTransaction.Entities;

namespace PostGreSqlTransaction.Interfaces;

public interface IAccountRepository : IBaseRepository<Account>
{
    //Task<IEnumerable<Account>> GetAccountsByUserId(Guid userId);

    //Task<User> GetUserByIdAsync(Guid userId);
    
    Task<IEnumerable<Account>> AccountsByUser(Guid ownerId);
}