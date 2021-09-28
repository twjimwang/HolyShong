using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class DeliverViewModel
    {
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string OrderCode { get; set; }
        public List<OrderProducts> OrderProducts { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustormerNotes { get; set; }
    }

    public class OrderProducts
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
    }

}