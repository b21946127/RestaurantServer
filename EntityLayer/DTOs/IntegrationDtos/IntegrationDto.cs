using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.IntegrationDtos
{
    public class IntegrationDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool IsAllergen { get; set; } 
    }
}
