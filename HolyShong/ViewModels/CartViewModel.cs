using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public string StoreName { get; set; }
        public List<Item> Items { get; set; }
        public int TotalPrice { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string ProductName { get; set; }
        public List<ProductOptionCard> ProductOptionCards { get; set; }
        public decimal UnitPrice { get; set; }
       
    }

    public class ProductOptionCard
    {
        public int ProductOptionId { get; set; }

        /// <summary>
        /// 甜度選擇
        /// </summary>
        public string ProductOptionName { get; set; }
        /// <summary>
        /// 無糖或少糖
        /// </summary>
        public string ProductOptionDetail { get; set; }
    }
}