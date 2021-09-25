using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class OrderDeliverViewModel
    {
        public string DeliverName { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string RestaurantName { get; set; }
        public List<OrderList> OderLists { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderList
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductTotal { get; set; }

    }
}