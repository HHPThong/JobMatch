using FPTJobMatch.Models;

namespace FPTJobMatch.Repository.IRepository
{
    public interface IStatusRepository:IRepository<Status>
    {
        public void Update(Status status);
    }
}
