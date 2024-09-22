using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Menu
    {
        [Key]
        public int Id { get;  set; }

        public DayOfWeekEnum DayOfWeek { get; set; }

        public ICollection<MenuCategory> MenuCategories { get; set; }

    }
}
