using EntityLayer.DTOs.IntegrationDtos;
using EntityLayer.DTOs.MenuOptionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.MenuItemDtos
{
    public class UpdateMenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Portion { get; set; }
        public decimal Price { get; set; }
        public List<IntegrationDto> Integrations { get; set; }
        public List<MenuItemOptionDto> Options { get; set; }
    }
}
