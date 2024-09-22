using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {

        Task DeleteAsync(T p);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task InsertAsync(T p);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(T p);

        List<T> List();
        void Insert(T p);
        void Delete(T p);
        void Update(T p);

        T Get(Expression<Func<T, bool>> filter);

        List<T> List(Expression<Func<T, bool>> filter);

    }
}
