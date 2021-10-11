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
            RestaurantVM result = new RestaurantVM()
            {
               // ProductAreaList = new List<ProductAreaList>()              
            };

          
            //店家
            var store = _repo.GetAll<Store>().FirstOrDefault((x) => x.StoreId == id);
            if (store == null)
            {
                return result;
            }

            //評分
            result.Score = _repo.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => (decimal?)x.ScorePoint) == null ? 0 : _repo.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => x.ScorePoint);

            //開店時間??


            //商店分類
            result.StoreCategoryName = _repo.GetAll<StoreCategory>().First(x => x.StoreCategoryId == store.StoreCategoryId).Name;

            //營業時間
            var businessHours = _repo.GetAll<Businesshours>().Where(x => x.StoreId == store.StoreId);
            List<Businesshours> BusinesshoursList = new List<Businesshours>();
            //把7筆抓出來(一~日)
            foreach (var item in businessHours)
            {
                BusinesshoursList.Add(item);
            }
            result.BusinesshoursList = BusinesshoursList;



            //產品類別(錨點)
            ProductArea ProductArea = new ProductArea();
            var productCategories = _repo.GetAll<ProductCategory>().Where((x) => x.StoreId == store.StoreId);
            //2類
            foreach (var item in productCategories)
            {
                ProductArea.ProductCategoryName = item.Name;
            }

            var products = _repo.GetAll<Product>().Where(x => productCategories.Select(y => y.ProductCategoryId).Contains(x.ProductCategoryId)).GroupBy(x=>x.ProductCategoryId);

            List<ProductArea> ProductAreaList = new List<ProductArea>();


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







            result.StoreId = store.StoreId;
            result.StoreName = store.Name;
            result.StorePicture = store.Img;
            result.StoreAddress = store.Address;
          

           


            return result;
        }

    }
}