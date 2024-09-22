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
    public class EfMenuDal : GenericRepository<Menu>, IMenuDal
    {

        Context _context;

        public EfMenuDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Menu> GetByAll(Expression<Func<Menu, bool>> predicate)
        {
            return await _context.Menus
                .Include(menu => menu.MenuCategories)
                        .ThenInclude(mci => mci.MenuItems)
                          .ThenInclude(mO => mO.Options)
                .Include(menu => menu.MenuCategories)
                        .ThenInclude(mci => mci.MenuItems)
                          .ThenInclude(mO => mO.Integrations)

                 .Include(menu => menu.MenuCategories)
                    .ThenInclude(mci => mci.MenuItemSets)
                        .ThenInclude(mis => mis.MenuItemMenuItemSets)
                .SingleOrDefaultAsync(predicate);
        }

    }
}
