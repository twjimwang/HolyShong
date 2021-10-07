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

        //主分類提取
        public List<ViewModels.StoreCategory> GetAllStoreCategories()
        {
            var result = new List<ViewModels.StoreCategory>();
            var storecategories = _storecategoryRespository.GetAll<Models.HolyShongModel.StoreCategory>();
            foreach (var item in storecategories)
            {
                var temp = new ViewModels.StoreCategory();
                temp.StoreCategoryId = item.StoreCategoryId;
                temp.StoreCategoryImg = item.Img;
                temp.StoreCategoryName = item.Name;
                result.Add(temp);
            }
            return result;
        }


        //產生亂數
        public int GetRandomNumber()
        {
            return 0;
        }
    }
}