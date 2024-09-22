using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class MenuItemMenuItemSet
    {
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int MenuItemSetId { get; set; }
        public MenuItemSet MenuItemSet { get; set; }

    }
}
