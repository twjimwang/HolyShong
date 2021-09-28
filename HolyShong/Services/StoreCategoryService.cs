using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;

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

        //主分類提取
        public List<StoreCategory> GetAllStoreCategories()
        {

            var result = new List<StoreCategory>();
            var storecategories = _storecategoryRespository.GetAll<StoreCategory>();
            foreach (var item in storecategories)
            {
                var storecategory = new StoreCategory();
                storecategory.StoreCategoryId = item.StoreCategoryId;
                storecategory.StoreCategoryImg = item.StoreCategoryImg;
                storecategory.StoreCategoryName = item.StoreCategoryName;
                result.Add(storecategory);
            }
            return result;
        }

    }
}