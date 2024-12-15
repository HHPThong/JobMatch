using FPTJobMatch.Data;
using FPTJobMatch.Models;
using Microsoft.EntityFrameworkCore;

namespace FPTJobMatch.Repository.IRepository
{
	public class AppUserRepository : Repository<ApplicationUser>, IAppUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
	public AppUserRepository(ApplicationDBContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
	public void Update(ApplicationUser entity)
	{
		_dbContext.ApplicationUsers.Update(entity);
	}
}
}
