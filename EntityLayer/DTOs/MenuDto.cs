namespace RestaurantServer.DTOs
{
    public class MenuDto
    {
        public string DayOfWeek { get; set; }
        public List<MenuCategoryDto> MenuCategories { get; set; }
    }
}
