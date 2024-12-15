using FPTJobMatch.Models;
using Microsoft.AspNetCore.Builder;
using System.Linq.Expressions;

namespace FPTJobMatch.Repository.IRepository
{
    public interface IApplicationJobRepository:IRepository<ApplicationJob>
    {
		IEnumerable<ApplicationJob> GetAllJobApp(Expression<Func<ApplicationJob, bool>> filter = null);
		public void Update(ApplicationJob entity);
    }
}
