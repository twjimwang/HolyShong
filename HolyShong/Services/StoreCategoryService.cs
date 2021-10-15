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
        public List<HomeStoreCategory> GetAllStoreCategories()
        {
            var result = new List<HomeStoreCategory>();
            var storecategories = _storecategoryRespository.GetAll<Models.HolyShongModel.StoreCategory>();
            foreach (var item in storecategories)
            {
                var temp = new HomeStoreCategory();
                temp.StoreCategoryId = item.StoreCategoryId;
                temp.StoreCategoryImg = item.Img;
                temp.StoreCategoryName = item.Name;
                result.Add(temp);
            }
            return result;
        }


        //產生亂數
        public int[] GetRandomNumber()
        {
            int[] randomArray = new int[5];
            Random number = new Random();  //產生亂數初始值
            for (int i = 0; i < 5; i++)
            {
                randomArray[i] = number.Next(1, 10);   //亂數產生，亂數產生的範圍是1~9

                for (int j = 0; j < i; j++)
                {
                    while (randomArray[j] == randomArray[i])    //檢查是否與前面產生的數值發生重複，如果有就重新產生
                    {
                        j = 0;  //如有重複，將變數j設為0，再次檢查 (因為還是有重複的可能)
                        randomArray[i] = number.Next(1, 16);   //重新產生，存回陣列，亂數產生的範圍是1~15
                    }
                }
            }
            return randomArray;
        }
    }
}