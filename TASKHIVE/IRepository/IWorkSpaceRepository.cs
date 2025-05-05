using TASKHIVE.Model;

namespace TASKHIVE.IRepository
{
    public interface IWorkSpaceRepository : IGenericRepository<WorkSpace>
    {
        Task update(WorkSpace entity);
    }
}
