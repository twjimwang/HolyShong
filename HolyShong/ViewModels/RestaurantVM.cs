using HolyShong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class RestaurantVM
    {            
        //ProductCategory
        public int ProductCategoryID { get; set; }

        public int PcStoreId { get; set; }
        public int ProductCategorySort { get; set; }
        public string ProductCategoryName { get; set; }
        public List<ProductCategory> PcgName { get; set; }

        public string SupplyTime { get; set; }

        //Store
        public int StoreId { get; set; }

        public int StoreCategoryId { get; set; }
      
        public string StoreName { get; set; }
      
        public string StorePicture { get; set; }      
     
        public string StoreAddress { get; set; }

        //Product       
        public List<Product> Products { get; set; }//需要很多商品,所以用成集合
        public List<ProductCategory> Sort { get; set; }
        //Score
        public int ScoreId { get; set; }

        public int ScoreStoreId { get; set; }

        public int OrderId { get; set; }
    
        public decimal Score1 { get; set; }
        //StoreCategory                     

       
        public string StoreCategoryName { get; set; }

      
        public string StoreCategoryKeyWord { get; set; }

       
        public string StoreCategoryImg { get; set; }
    }


}