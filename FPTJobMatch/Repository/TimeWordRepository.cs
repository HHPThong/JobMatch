using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FPTJobMatch.Repository
{
	public class TimeWordRepository: Repository<TimeWork>, ITimeWorkRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public TimeWordRepository(ApplicationDBContext dbContext) : base(dbContext) { _dbContext = dbContext;}	
		public void Update (TimeWork timeWork) { _dbContext.timeWork.Update(timeWork);}
	}
}
