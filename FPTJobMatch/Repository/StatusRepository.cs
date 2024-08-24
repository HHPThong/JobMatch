using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;

namespace FPTJobMatch.Repository
{
    public class StatusRepository: Repository<Status>, IStatusRepository
    {
        public readonly ApplicationDBContext _dbContext;
        public StatusRepository(ApplicationDBContext dbContext) : base(dbContext) { _dbContext = dbContext; }
        public void Update (Status status) { _dbContext.status.Update(status); }
    }
}
