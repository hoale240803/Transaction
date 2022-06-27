using Microsoft.EntityFrameworkCore.Storage;
using PostGreSqlTransaction.Repositories.Contracts;

namespace PostGreSqlTransaction.Entities
{
    public class EntityDbTransaction : IDbTransaction
    {
        private IDbContextTransaction _transaction;

        public EntityDbTransaction(TransContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
