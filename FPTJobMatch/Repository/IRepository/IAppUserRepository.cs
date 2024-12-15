using FPTJobMatch.Models;

namespace FPTJobMatch.Repository.IRepository
{
	public interface IAppUserRepository : IRepository<ApplicationUser>
	{
		void Update(ApplicationUser entity);
	}
}
