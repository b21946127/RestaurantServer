using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class MenuItemSet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public MenuCategory MenuCategory { get; set; }
        public ICollection<MenuItemMenuItemSet> MenuItemMenuItemSets { get; set; }

    }
}
