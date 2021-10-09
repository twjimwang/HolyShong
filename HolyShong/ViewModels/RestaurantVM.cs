using HolyShong.Models.HolyShongModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class RestaurantVM
    {
        public int StoreId { get; set; }
        public string StorePicture { get; set; }
        public string StoreName { get; set; }
        public decimal? Score1 { get; set; }

        public string StoreCategoryName { get; set; }

        public string StoreAddress { get; set; }

        public string SupplyTime { get; set; }

        public List<ProductAreaList> ProductAreaList { get; set; }   
    }


    /// <summary>
    /// 專區系列
    /// </summary>
    public class ProductAreaList
    {
        public List<ProductArea> ProductArea { get; set; }
    }
    /// <summary>
    /// 專區
    /// </summary>
    public class ProductArea
    {
        public string ProductCategoryName { get; set; }
        public List<ProductCard> Products { get; set; }
    }
    /// <summary>
    /// 商品卡片
    /// </summary>
    public class ProductCard
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public List<ProductOptionCard> ProductOptions { get; set; }
    }
    /// <summary>
    /// 選項區
    /// </summary>
    public class ProductOptionCard
    {
        public string ProductOptionName { get; set; }
        public List<string> ProductOptionDetails { get; set; }
    }

}