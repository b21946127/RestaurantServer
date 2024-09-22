using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Portion { get; set; }

        public decimal Price { get; set; }

        public MenuCategory MenuCategory { get; set; }

        public ICollection<Integration> Integrations { get; set; }

        public ICollection<MenuItemOption> Options { get; set; }

        public ICollection<MenuItemMenuItemSet> MenuItemMenuItemSets { get; set; }

    }
}
