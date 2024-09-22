using EntityLayer.DTOs.MenuItemDtos;
using EntityLayer.DTOs.MenuItemSetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.MenuCategoryDtos
{
    public class UpdateMenuCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<MenuItemDto> MenuItems { get; set; }

    }
}
