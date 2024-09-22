using EntityLayer.DTOs.IntegrationDtos;
using EntityLayer.DTOs.MenuItemMenuItemSetDtoS;
using EntityLayer.DTOs.MenuOptionDtos;

namespace EntityLayer.DTOs.MenuItemDtos
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Portion { get; set; }
        public decimal Price { get; set; }
        public List<MenuItemOptionDto> Options { get; set; }
        public List<IntegrationDto> Integrations { get; set; }


    }
}
