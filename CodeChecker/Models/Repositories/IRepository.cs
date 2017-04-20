using System.Linq;
using CodeChecker.Models.Models;

namespace CodeChecker.Models.Repositories
{
    public interface IRepository<T> where T: BaseModel
    {
        T Get(long id);
        IQueryable<T> Query();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Recover(T entity);
    }
}