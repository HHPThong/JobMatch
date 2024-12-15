namespace FPTJobMatch.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository CategoryRepository { get; }
		IJobRepository JobRepository { get; }
		IAppUserRepository AppUserRepository { get; }
		IApplicationJobRepository JobApplicationRepository { get; }
		void Save();
	}
}
