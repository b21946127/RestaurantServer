using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
    public class UpdateMenuDto
    {
        public string DayOfWeek { get; set; }
        public List<UpdateMenuCategoryDto> MenuCategories { get; set; }
    }
}
