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
    public class EfMenuCategoryDal : GenericRepository<MenuCategory>, IMenuCategoryDal

    {
        Context _context;

        public EfMenuCategoryDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<MenuCategory> GetByAllAsync(Expression<Func<MenuCategory, bool>> predicate)
        {
            return await _context.MenuCategories
                .Include(menuCategory => menuCategory.Menu)
                .Include(menuCategory => menuCategory.MenuItems)
                .Include(menuCategory => menuCategory.MenuItemSets)
                .SingleOrDefaultAsync(predicate);
        }
    }
}   
