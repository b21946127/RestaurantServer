﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMenuItemMenuItemSetDal : GenericRepository<MenuItemMenuItemSet>, IMenuItemMenuItemSetDal
    {
        public EfMenuItemMenuItemSetDal(Context context) : base(context)
        {
        }
    }
}
