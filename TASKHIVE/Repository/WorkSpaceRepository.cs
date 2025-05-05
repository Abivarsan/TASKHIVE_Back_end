using TASKHIVE.Data;
using TASKHIVE.IRepository;
using TASKHIVE.Model;

namespace TASKHIVE.Repository
{
    public class WorkSpaceRepository : GenericRepository<WorkSpace>, IWorkSpaceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkSpaceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task update(WorkSpace entity)
        {
            _dbContext.WorkSpaces.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
