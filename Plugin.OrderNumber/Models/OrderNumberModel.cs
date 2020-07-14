using System;
using System.ComponentModel.DataAnnotations;

namespace Achievecreative.Commerce.Plugin.OrderNumber.Models
{
    public class OrderNumberModel
    {
        [Key]
        public string OrderNumber { get; set; }

        public DateTime DateTime { get; set; }
    }
}
