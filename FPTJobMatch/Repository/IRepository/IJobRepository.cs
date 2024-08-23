using FPTJobMatch.Models;

namespace FPTJobMatch.Repository.IRepository
{
    public interface IJobRepository:IRepository<Job>
    {
        public void Update(Job job);
    }
}
