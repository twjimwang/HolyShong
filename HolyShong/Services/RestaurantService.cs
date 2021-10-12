using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class RestaurantService
    {
        private readonly HolyShongRepository _repo;
        public RestaurantService()
        {
            _repo = new HolyShongRepository();
        }

        public RestaurantVM GetRestaurant(int? id)
        {
            RestaurantVM result = new RestaurantVM();
            //店家
            var store = _repo.GetAll<Store>().FirstOrDefault((x) => x.StoreId == id);
            if (store == null)
            {
                return result;
            }

            result.StoreId = store.StoreId;
            result.StoreName = store.Name;
            result.StorePicture = store.Img;
            result.StoreAddress = store.Address;

            //商店分類
            result.StoreCategoryName = _repo.GetAll<StoreCategory>().First(x => x.StoreCategoryId == store.StoreCategoryId).Name;

            //評分
            result.Score = _repo.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => (decimal?)x.ScorePoint) == null ? 0 : _repo.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => x.ScorePoint);


            //開店時間&營業時間
            List<Businesshour> BusinesshourList = new List<Businesshour>();
            var businessHours = _repo.GetAll<Businesshours>().Where(x => x.StoreId == store.StoreId);

            DateTime dt = DateTime.Now;
            int week = dt.DayOfWeek.GetHashCode();
            //把7筆抓出來(一~日)
            TimeSpan todayOpening = new TimeSpan();

            foreach (var item in businessHours)
            {
                Businesshour Businesshour = new Businesshour();

                Businesshour.WeekDay = convertToChinese(item.WeekDay.ToString()) ;
                Businesshour.OpenTime = item.OpenTime.ToString(@"hh\:mm");
                Businesshour.CloseTime = item.CloseTime.ToString(@"hh\:mm");
                BusinesshourList.Add(Businesshour);
                if (week == item.WeekDay)
                {
                    todayOpening = item.OpenTime;
                }

            }

            result.BusinesshourList = BusinesshourList;//所有營業時間&日期
            result.todayOpening = todayOpening.ToString(@"hh\:mm");//去秒數

            //專區
            








            ////產品類別(錨點)
            //ProductArea ProductArea = new ProductArea();
            //var productCategories = _repo.GetAll<ProductCategory>().Where((x) => x.StoreId == store.StoreId);
            ////2類
            //foreach (var item in productCategories)
            //{
            //    ProductArea.ProductCategoryName = item.Name;
            //}

            //var products = _repo.GetAll<Product>().Where(x => productCategories.Select(y => y.ProductCategoryId).Contains(x.ProductCategoryId)).GroupBy(x => x.ProductCategoryId);

        


            //foreach (var item in products)
            //{
            //    List<ProductCard> ProductsList = new List<ProductCard>();
            //    foreach (var item1 in item)
            //    {
            //        ProductCard ProductCard = new ProductCard();
            //        ProductCard.ProductName = item1.Name;
            //        ProductCard.UnitPrice = item1.UnitPrice;
            //        ProductCard.Img = item1.Img;
            //        ProductsList.Add(ProductCard);
            //    }
            //    ProductAreaList
            //}

            //產品(productCategories是集合)












            return result;
        }
        public string convertToChinese(string week)
        {
            string chinese = week.ToString().Replace("1", "一").Replace("2", "二").Replace("3", "三")
                .Replace("4", "四").Replace("5", "五").Replace("6", "六").Replace("7", "七").Replace("0", "日");
            return chinese;
        }
    }
}