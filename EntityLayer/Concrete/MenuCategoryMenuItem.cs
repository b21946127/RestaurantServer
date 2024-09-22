using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class MenuCategoryMenuItem
    {
        [Key]
        public int Id { get; set; }

        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
