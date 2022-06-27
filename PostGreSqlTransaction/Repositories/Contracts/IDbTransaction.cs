namespace PostGreSqlTransaction.Repositories.Contracts
{
    public interface IDbTransaction:IDisposable
    {

        void Commit();

        void Rollback();
    }
}
