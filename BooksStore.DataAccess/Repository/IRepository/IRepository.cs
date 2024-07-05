using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? include = null);
        T Get(Expression<Func<T, bool>> expression, string? include = null, bool tracked = false);
        void Added(T entity);
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
