using BusinessLayer.Abstract;
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
    public class EfMenuCategoryMenuItemDal : GenericRepository<MenuCategoryMenuItem>, IMenuCategoryMenuItemDal
    {
        public EfMenuCategoryMenuItemDal(Context context) : base(context)
        {
        }
    }
}
