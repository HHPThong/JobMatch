using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;

namespace FPTJobMatch.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public JobRepository(ApplicationDBContext dbContext) : base(dbContext) { _dbContext = dbContext; }
        public void Update(Job job) { _dbContext.jobs.Update(job); }

    }
}
