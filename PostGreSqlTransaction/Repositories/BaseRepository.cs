using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Interfaces;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PostGreSqlTransaction.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected TransContext _transContext { get; set; }
        public BaseRepository(TransContext transContext)
        {
            _transContext = transContext;
        }
        public IQueryable<T> FindAll()
        {
            return _transContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _transContext.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            _transContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _transContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _transContext.Set<T>().Remove(entity);
        }
    }
}
