using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class OrderListViewModel
    {
        /// <summary>
        /// 訂單運送日期
        /// </summary>
        public DateTime DeliverDate { get; set; }
        /// <summary>
        /// 訂單狀況(0未完成 1完成)
        /// </summary>
        public int OrderState { get; set; } 
        /// <summary>
        /// 消費者訂單備註
        /// </summary>
        public string RestaurantName { get; set; }
        public List<OrderProduct> OrderLists { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderProduct
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public List<OrderOptionDetail> OrderOptions { get; set; }

    }

    public class OrderOptionDetail
    {
        public string ProdectOptionName { get; set; }

    }

}