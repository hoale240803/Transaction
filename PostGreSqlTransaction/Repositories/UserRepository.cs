using Microsoft.EntityFrameworkCore;
using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;

namespace PostGreSqlTransaction.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly TransContext _context;


        public UserRepository(TransContext transContext) : base(transContext)
        {
        }
        
        public async Task<IEnumerable<User>> GetAllUsersAsync() 
        { 
            return await FindAll()
                .OrderBy(ow => ow.Name)
                .ToListAsync(); 
        }

        public async Task<User> GetUserByIdAsync(Guid UserId)
        {
            return await FindByCondition(User => User.Id.Equals(UserId))
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserWithDetailsAsync(Guid UserId)
        {
            return await FindByCondition(User => User.Id.Equals(UserId))
                .Include(ac => ac.Accounts)
                .FirstOrDefaultAsync();
        }

        public void CreateUser(User User)
        {
            Create(User);
        }

        public void UpdateUser(User User)
        {
            Update(User);
        }

        public void DeleteUser(User User)
        {
            Delete(User);
        }
    }
}
