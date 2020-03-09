using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CWheelsApi.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Title should not be null or empty")]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
