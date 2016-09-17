using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceStation.Models
{
    public class ServiceClient
    {
        public int ServiceClientID { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [ValidateDateRange(1900, 2017)]
        public DateTime DateofBirth { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public virtual List<Car> Cars { get; set; }

    }
}
