using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;

namespace FPTJobMatch.Repository
{
    public class ApplicationJobRepository : Repository<ApplicationJob>, IApplicationJobRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ApplicationJobRepository(ApplicationDBContext dbContext) : base(dbContext){ _dbContext = dbContext; }
        public void Update(ApplicationJob application) { _dbContext.apps.Update(application); }

    }
}
