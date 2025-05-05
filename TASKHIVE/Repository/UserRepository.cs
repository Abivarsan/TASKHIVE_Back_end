using Microsoft.EntityFrameworkCore;
using TASKHIVE.Data;
using TASKHIVE.IRepository;
using TASKHIVE.Model;

namespace TASKHIVE.Repository
{
   
        public class UserRepository : GenericRepository<User>, IUserRepository
        {
            private readonly ApplicationDbContext _dbContext;

            public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task update(User entity)
            {
                _dbContext.Users.Update(entity);
                await _dbContext.SaveChangesAsync();
            }

        public async Task<User> Login(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.password))
            {
                return null;
            }
            return user;
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users
                .Include(u => u.Role) // Ensure Role is included
                .FirstOrDefaultAsync(a => a.email == email);
        }

    }
    
}
