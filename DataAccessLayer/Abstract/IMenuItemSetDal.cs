using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMenuItemSetDal: IRepository<MenuItemSet>
    {
        Task<MenuItemSet> GetByAllAsync(Expression<Func<MenuItemSet, bool>> predicate);
    }
}
