using EntityLayer.DTOs.MenuCategoryDtos;

namespace EntityLayer.DTOs.MenuDtos
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public List<MenuCategoryDto> MenuCategories { get; set; }
    }
}
