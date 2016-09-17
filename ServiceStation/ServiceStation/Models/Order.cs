using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceStation.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DefaultValue(true)]
        [ValidateDateRange(1768, 2017)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Order Amount")]
        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal OrderAmount { get; set; }
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Order Status")]
        [DefaultValue(1)]
        public Status OrderStatus { get; set; }

        public int CarID { get; set; }
        public virtual Car Car { get; set; }

        public Order()
        {
            Date = DateTime.Now;
        }
    }

    public enum Status
    {
        [Display(Name = "Completed")]
        Completed = 0,
        [Display(Name = "In Progress")]
        InProgress = 1,
        [Display(Name = "Cancelled")]
        Cancelled = 2
    }
}