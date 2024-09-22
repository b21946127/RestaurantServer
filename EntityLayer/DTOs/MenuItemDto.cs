using EntityLayer.DTOs;

namespace RestaurantServer.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Portion { get; set; }
        public decimal Price { get; set; }

        public List<MenuItemOptionDto> Options { get; set; }

    }
}
