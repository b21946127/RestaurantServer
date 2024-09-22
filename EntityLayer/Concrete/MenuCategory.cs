using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class MenuCategory
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }

        public ICollection<MenuItemSet> MenuItemSets { get; set; }

        public Menu Menu { get; set; }
    }
}
