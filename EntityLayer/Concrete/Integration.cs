using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Integration
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; set; } 

        public Boolean IsAllergen { get; set; }

    }
}
