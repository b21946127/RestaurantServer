﻿using DataAccessLayer.Abstract;
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
    public class EfMenuItemDal : GenericRepository<MenuItem>, IMenuItemDal
    {

        Context _context;
        public EfMenuItemDal(Context context) : base(context)
        {
            _context = context;

        }

        public async Task<MenuItem> GetByAll(Expression<Func<MenuItem, bool>> predicate)
        {
            return await _context.MenuItems
                .Include(MenuItemMenuItemSet => MenuItemMenuItemSet.MenuItemMenuItemSets)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
