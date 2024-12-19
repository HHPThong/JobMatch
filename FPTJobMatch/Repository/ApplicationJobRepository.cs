using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using System.Linq.Expressions;

namespace FPTJobMatch.Repository
{
    public class ApplicationJobRepository : Repository<ApplicationJob>, IApplicationJobRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public ApplicationJobRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public IEnumerable<ApplicationJob> GetAllAppJob(Expression<Func<ApplicationJob, bool>> filter = null)
		{
			IQueryable<ApplicationJob> query = _dbContext.apps;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.ToList();
		}

		public void Update(ApplicationJob entity)
		{
			_dbContext.apps.Update(entity);
		}
	}
}

