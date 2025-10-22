using System.Linq.Expressions;
using lab1mvc.context;

namespace lab1mvc.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes); // ✅ add this line

        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
