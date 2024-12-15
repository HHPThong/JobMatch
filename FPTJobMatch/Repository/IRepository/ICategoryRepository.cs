using FPTJobMatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace FPTJobMatch.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category entity);
	}
}
