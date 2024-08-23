using FPTJobMatch.Models;

namespace FPTJobMatch.Repository.IRepository
{
    public interface IApplicationJobRepository:IRepository<ApplicationJob>
    {
        public void Update(ApplicationJob application);
    }
}
