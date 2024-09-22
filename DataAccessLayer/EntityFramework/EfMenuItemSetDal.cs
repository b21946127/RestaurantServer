using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMenuItemSetDal : GenericRepository<MenuItemSet>, IMenuItemSetDal
    {
        Context _context;
        public EfMenuItemSetDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<MenuItemSet> GetByAllAsync(Expression<Func<MenuItemSet, bool>> predicate)
        {
            return await _context.MenuItemSets
                .Include(MenuItemSet => MenuItemSet.MenuItemMenuItemSets)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
