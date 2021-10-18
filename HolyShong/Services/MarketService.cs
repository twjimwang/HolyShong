using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class MarketService
    {
        private readonly HolyShongRepository _repo;
        public MarketService()
        {
            _repo = new HolyShongRepository();
        }
        public MarketViewModel GetEventRestaurant(int DiscountId)
        {
            var result = new MarketViewModel();
            #region 一
            ////找一個優惠
            //var discount = _repo.GetAll<Discount>().FirstOrDefault(d => d.DiscountId == DiscountId);
            //result.EventTitle = discount.Title;
            //result.EventContent = discount.Contents;
            //result.EventStart = (DateTime)discount.StartTime;
            //result.EventEnd = (DateTime)discount.EndTime;

            //result.eventRestaurants = new List<EventRestaurant>();

            ////找一個優惠下面所有店家
            //var discountStore = _repo.GetAll<DiscountStroe>().FirstOrDefault(ds => ds.DiscountId == discount.DiscountId);
            //var store = _repo.GetAll<Store>().Where(s => s.StoreId == discountStore.StoreId);
            //foreach(var s in store)
            //{
            //    var sTemp = new EventRestaurant()
            //    {
            //        RestaurantImg = s.Img,
            //        RestaurantName = s.Name
            //    };
            //    result.eventRestaurants.Add(sTemp);
            //}

            #endregion

            #region 二
            //方法二
            //找一個優惠
            var discount = _repo.GetAll<Discount>().FirstOrDefault(d => d.DiscountId == DiscountId);
            var discountStore = _repo.GetAll<DiscountStroe>().Where(ds=>ds.DiscountId==discount.DiscountId);
            var store = _repo.GetAll<Store>().Where(s => discountStore.Select(ds => ds.StoreId).Contains(s.StoreId));

            result.EventTitle = discount.Title;
            result.EventContent = discount.Contents;
            result.EventStart = (DateTime)discount.StartTime;
            result.EventEnd = (DateTime)discount.EndTime;
            
            result.eventRestaurants = new List<EventRestaurant>();
            //卡片區塊
            foreach (var s in store)
            {
                var sTemp = new EventRestaurant()
                {
                    RestaurantImg = s.Img,
                    RestaurantName = s.Name,
                  
                };
                result.eventRestaurants.Add(sTemp);
            }
            #endregion

            return result;
        }
    }
}