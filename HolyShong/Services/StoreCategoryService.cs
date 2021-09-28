using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;

namespace HolyShong.Services
{
    public class StoreCategoryService
    {
        //初始化
        private readonly HolyShongRepository _storecategoryRespository;
        public StoreCategoryService()
        {
            _storecategoryRespository = new HolyShongRepository();
        }
        /// <summary>
        /// 從資料庫取出我要的資料，並且轉成ViewModel(Id、商品名、圖片)給Controller的Action調用
        /// </summary>
        /// <returns>ProductIndexViewModel</returns>
        public List<HomeViewModel> GetAllStoreCategories()
        {
            
            var result = new List<HomeViewModel>();
            var storecategories = _storecategoryRespository.GetAll<StoreCategory>();
           
            foreach (var item in storecategories)
            {
                var storecategory = new HomeViewModel();
                storecategory.StoreCategoryName = item.Name;
                storecategory.StoreCategoryImg = item.Img;
                result.Add(storecategory);
            }
            return result;
        }
    }
}