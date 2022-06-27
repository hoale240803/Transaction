using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;

namespace PostGreSqlTransaction.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TransContext _context;

        public UserRepository(TransContext _context)
        {
            _context = _context;
        }
    }
}
