using FPTJobMatch.Data;
using FPTJobMatch.Repository.IRepository;

namespace FPTJobMatch.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDBContext _dbContext;
		public ICategoryRepository CategoryRepository { get; private set; }
		public IJobRepository JobRepository { get; private set; }
		public IAppUserRepository AppUserRepository { get; private set; }
		public IApplicationJobRepository JobApplicationRepository { get; private set; }
		public UnitOfWork(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
			CategoryRepository = new CategoryRepository(dbContext);
			JobRepository = new JobRepository(dbContext);
			JobApplicationRepository = new ApplicationJobRepository(dbContext);
			AppUserRepository = new AppUserRepository(dbContext);
		}
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
