using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMenuDal : IRepository<Menu>
    {
        Task<Menu> GetWithCategoriesAsync(Expression<Func<Menu, bool>> predicate);

    }
}
