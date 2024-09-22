using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Integration
    {
        [Key]
        public string Id { get; private set; }

        [Required]
        public string Name { get; set; } 

    }
}
