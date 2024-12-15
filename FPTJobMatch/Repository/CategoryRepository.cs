using FPTJobMatch.Data;
using FPTJobMatch.Models;
using FPTJobMatch.Repository.IRepository;

namespace FPTJobMatch.Repository.IRepository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDBContext _dbContext;
		public CategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public void Update(Category entity)
		{
			_dbContext.Categories.Update(entity);
		}
	}
}
