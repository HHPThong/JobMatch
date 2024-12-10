using FPTJobMatch.Models;

namespace FPTJobMatch.Repository.IRepository
{
	public interface ITimeWorkRepository:IRepository<TimeWork>
	{
		public void Update (TimeWork TimeWork);
	}
}
