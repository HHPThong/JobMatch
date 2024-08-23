using System.Linq.Expressions;

namespace FPTJobMatch.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll(string? includeProperty = null);
        public T Get(Expression<Func<T, bool>> predicate, string? includeProperty = null);
        void Add(T entity);
        void Delete(T entity);
        void Save();
    }
}
