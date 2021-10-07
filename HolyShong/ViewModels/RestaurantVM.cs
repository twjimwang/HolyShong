using HolyShong.Models.HolyShongModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class RestaurantVM
    {            
        //ProductCategory
        public List<ProductCategory> productCategories { get; set; }

        public string SupplyTime { get; set; }

        //Store
        public int StoreId { get; set; }

        public string StoreName { get; set; }
      
        public string StorePicture { get; set; }      
     
        public string StoreAddress { get; set; }

        //Product       
        public List<Product> Products { get; set; }//需要很多商品,所以用成集合

    
        public decimal? Score1 { get; set; }
        //StoreCategory                     

       
        public string StoreCategoryName { get; set; }
    }


}