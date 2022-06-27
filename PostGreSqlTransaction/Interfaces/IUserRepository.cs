using PostGreSqlTransaction.Entities;

namespace PostGreSqlTransaction.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid UserId);
        Task<User> GetUserWithDetailsAsync(Guid UserId);
        void CreateUser(User User);
        void UpdateUser(User User);
        void DeleteUser(User User);
    }
}
