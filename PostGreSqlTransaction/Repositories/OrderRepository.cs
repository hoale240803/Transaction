using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;

namespace PostGreSqlTransaction.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TransContext _context;

        public OrderRepository(TransContext _context)
        {
            _context = _context;
        }
    }
}
