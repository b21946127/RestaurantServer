using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.MenuItemSetDtos
{
    public class UpdateMenuItemSetDto
    {
        public int Id { get; set; }
        public int MenuCategoryId { get; set; }
        public string Name { get; set; }
        public List<int> MenuItemIds { get; set; }
    }
}
