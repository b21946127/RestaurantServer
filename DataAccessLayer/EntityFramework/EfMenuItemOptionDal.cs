using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMenuItemOptionDal: GenericRepository<MenuItemOption>, IMenuItemOptionDal
    {
        public EfMenuItemOptionDal(Context context) : base(context)
        {
        }
    }
}
