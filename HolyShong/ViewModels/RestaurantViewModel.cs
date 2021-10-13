using HolyShong.Models.HolyShongModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class RestaurantViewModel
    {
        public int StoreId { get; set; }
        public string StorePicture { get; set; }
        public string StoreName { get; set; }
        public decimal? Score { get; set; }

        public string StoreCategoryName { get; set; }

        public string StoreAddress { get; set; }
        public List<ProductCard> ProductCards { get; set; }
        public List<ProductArea> ProductAreas { get; set; }










        public List<Businesshour> BusinesshourList { get; set; }
        public ProductAreaList ProductAreaList { get; set; }
        public string todayOpening { get; set; }
    }    


    /// <summary>
    /// 專區系列
    /// </summary>
    public class ProductAreaList
    {
        public List<ProductArea> ProductAreas { get; set; }
    }
    /// <summary>
    /// 專區
    /// </summary>
    public class ProductArea
    {
        public string ProductCategoryName { get; set; }
        public List<ProductCard> ProductCards { get; set; }
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
        public int ProductOptionId { get; set; }
        public string ProductOptionName { get; set; }
        public List<string> ProductOptionDetails { get; set; }
    }

    public class Businesshour
    {
        public string WeekDay { get; set; }
        public string OpenTime { get; set; } 

        public string CloseTime { get; set; }
       
    }
}