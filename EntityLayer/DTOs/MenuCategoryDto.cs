namespace RestaurantServer.DTOs
{
    public class MenuCategoryDto
    {

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<MenuItemDto> MenuItems { get; set; }

    }
}
