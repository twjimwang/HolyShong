using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;

namespace HolyShong.Services
{
    public class StoreService
    {
        //店家卡片
        public HomeViewModel GetAllStoresByStoreCategoryId()
        {
            var result = new HomeViewModel();
            //    {
            //        StoreCards = new List<StoreCard>(),
            //        StoreCategoryId = storecategoryId
            //    };
            //    var storecategory = _storecategoryRespository.GetAll<StoreCategory>().First(x => x.StoreCategoryId == storecategoryId);
            //    var stores = _storeRespository.GetAll<Store>().Where(x => x.StoreCategoryId == storecategoryId).ToList();

            //    var storeCards = stores.Select(s => new StoreCard
            //    {
            //        StoreCategoryName = storecategory.Name,
            //        StoreName = s.Name,
            //        StoreImg = s.Img,
            //        StoreId = s.StoreId
            //    }).ToList();

            //    result.StoreCards = storeCards;

            //    result.StoreCategoryName = storecategory.Name;

            return result;
            //}
        }
    }
}