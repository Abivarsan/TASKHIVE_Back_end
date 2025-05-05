using TASKHIVE.Model;

namespace TASKHIVE.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task update(User users);
        Task<User> Login(string email, string password);
        Task<User> GetByEmail(string email);
    }
}
