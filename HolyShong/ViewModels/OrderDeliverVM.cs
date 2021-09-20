using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class OrderDeliverVM
    {
        public string DeliverName { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}