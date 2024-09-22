namespace RestaurantServer.DTOs
{
    public class CreateMenuItemDto
    {
		public string Name { get; set; }
		public string? Portion { get; set; }
		public decimal Price { get; set; }
		public int MenuCategoryId { get; set; }
		public List<int> SubItems { get; set; }
	}
}
