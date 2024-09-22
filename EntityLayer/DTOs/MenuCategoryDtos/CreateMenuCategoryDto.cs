using EntityLayer.Concrete;
using EntityLayer.DTOs.MenuItemDtos;
using EntityLayer.DTOs.MenuItemSetDtos;

namespace EntityLayer.DTOs.MenuCategoryDtos
{
    public class CreateMenuCategoryDto
    {
        public string CategoryName { get; set; }

        public List<CreateMenuItemDto> MenuItems { get; set; }
    }
}
