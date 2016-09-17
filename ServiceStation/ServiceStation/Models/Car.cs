using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceStation.Models
{
    public class Car
    {
        public int CarID { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Car Make")]
        public string Make { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Car Model")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Year of production")]
        [Range(1768, 2016, ErrorMessage = "Set correct year ({1}-{2})")]
        public int Year { get; set; }        
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "VIN number")]
        public string VIN { get; set; }

        public int ServiceClientID { get; set; }
        public virtual ServiceClient ServiceClient { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}