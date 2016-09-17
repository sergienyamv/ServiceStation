using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceStation.Models
{
    public class ValidateDateRange : ValidationAttribute
    {
        public DateTime Greater { get; set; }
        public DateTime Less { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(String.Format("Error date ({0}-{1})", Greater, Less));
            }
            else if ((DateTime)value >= Greater && (DateTime)value <= Less)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(String.Format("Error date ({0}-{1})", Greater, Less));
            }
        }

        public ValidateDateRange(int year1, int year2)
        {
            Greater = new DateTime(year1, 1, 1);
            Less = new DateTime(year2, 1, 1);
        }
    }
}