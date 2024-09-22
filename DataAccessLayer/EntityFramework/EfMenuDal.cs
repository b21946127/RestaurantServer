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

        public async Task<Menu> GetWithCategoriesAsync(Expression<Func<Menu, bool>> predicate)
        {
            return await _context.Menus
                .Include(menu => menu.MenuCategories)
                    .ThenInclude(category => category.MenuCategoryItems) 
                        .ThenInclude(mci => mci.MenuItem)
                          .ThenInclude(mO => mO.Options)
                .SingleOrDefaultAsync(predicate);
        }

    }
}
