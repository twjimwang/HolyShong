using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HolyShong.ViewModels
{
          
    public class HolyCartViewModel
    {
        /// <summary>
        /// item數量
        /// </summary>
        [Key] 
        public int RecordId { get; set; }
        /// <summary>
        /// cart
        /// </summary>
        public int CartId { get; set; }

        public int MemberId { get; set; }
        /// <summary>
        /// store
        /// </summary>
        public string StoreName { get; set; }

        public List<CartItem> CartItems { get; set; }

        /// <summary>
        /// 消費者暫存地址
        /// </summary>
        public String TempAddress { get; set; }

        /// <summary>
        /// 消費者地址
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// 外送指示
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 預計運送時間
        /// </summary>
        public string ShippingTime { get; set; }
        /// <summary>
        /// 購物車數量
        /// </summary>
        public int Count { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsTablewares { get; set; }
        public bool IsPlasticbag { get; set; }
        public string Notes { get; set; }
    }

    public class CartItem
    {   
        /// <summary>
        /// item
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// product
        /// </summary>
        public string ProductName { get; set; }

        public List<ProductOptionCard> ProductOptionCards { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 下拉選單 商品數量
        /// </summary>
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
        

    }
    public class ProductOptionCard
    {   
        /// <summary>
        /// productOption
        /// </summary>
        public int? ProductOptionId { get; set; }
        /// <summary>
        /// 甜度選擇 productOption
        /// </summary>
        public string ProductOptionName { get; set; }
        /// <summary>
        /// 無糖或少糖 
        /// </summary>
        public string ProductOptionDetail { get; set; }
        /// <summary>
        /// 加價 productOptionDetail
        /// </summary>
        public decimal? AddPrice { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Special { get; set; }
    }

}