using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.DTOs.MenuCategoryDtos;

namespace EntityLayer.DTOs.MenuDtos
{
    public class CreateMenuDto
    {
        public string DayOfWeek { get; set; }
        public List<CreateMenuCategoryDto> MenuCategories { get; set; }
    }
}
