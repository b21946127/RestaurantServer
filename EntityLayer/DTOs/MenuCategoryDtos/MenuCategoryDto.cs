using System.Collections.Generic;
using EntityLayer.DTOs.MenuItemDtos;
using EntityLayer.DTOs.MenuItemSetDtos;

namespace EntityLayer.DTOs.MenuCategoryDtos
{
    public class MenuCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<MenuItemDto> MenuItems { get; set; }
        public List<MenuItemSetDto> MenuItemSets { get; set; }
    }
}
