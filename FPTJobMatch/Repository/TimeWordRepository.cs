using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;

namespace FPTJobMatch.Repository
{
	public class TimeWordRepository: Repository<TimeWork>, ITimeWorkRepository
	{
		public readonly ApplicationDBContext _bdContext;
		public TimeWordRepository(ApplicationDBContext bdContext): base(bdContext) {_bdContext = bdContext;}	
		public void Update (TimeWork timeWork) { _bdContext.timeWork.Update(timeWork);}
	}
}
