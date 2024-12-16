namespace FPTJobMatch.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository CategoryRepository { get; }
		IJobRepository JobRepository { get; }
		IAppUserRepository AppUserRepository { get; }
		IApplicationJobRepository ApplicationJobRepository { get; }
		void Save();
	}
}
