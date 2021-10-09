using HolyShong.Models.HolyShongModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class RestaurantVM
    {
        ////ProductCategory
        //public List<ProductCategory> productCategories { get; set; }

        //public string SupplyTime { get; set; }

        ////Store
        //public int StoreId { get; set; }

        //public string StoreName { get; set; }

        //public string StorePicture { get; set; }

        //public string StoreAddress { get; set; }

        ////Product       
        //public List<Product> Products { get; set; }//需要很多商品,所以用成集合


        //public decimal? Score1 { get; set; }
        ////StoreCategory                     


        //public string StoreCategoryName { get; set; }
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