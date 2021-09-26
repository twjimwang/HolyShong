using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class OrderDeliverViewModel
    {
        public string DeliverName { get; set; }
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 消費者訂單備註
        /// </summary>
        public string CustomerNotes { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public List<OrderList> OderLists { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderList
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

    }
}