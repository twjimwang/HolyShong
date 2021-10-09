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
                ProductAreaList = new List<ProductAreaList>()              
            };

            //店家
            var store = _repo.GetAll<Store>().FirstOrDefault((x) => x.StoreId == id);
            if (store == null)
            {
                return result;
            }
            //產品類別(錨點)
            var productCategories = _repo.GetAll<ProductCategory>().Where((x) => x.StoreId == store.StoreId);
            //產品
            var products = _repo.GetAll<Product>().Where(x => productCategories.Select(y => y.ProductCategoryId).Contains(x.ProductCategoryId));
            var businessHours = _repo.GetAll<Businesshours>().Where(x => x.StoreId == store.StoreId);
            result.StoreId = store.StoreId;
            result.StoreName = store.Name;
            result.StorePicture = store.Img;
            result.StoreAddress = store.Address;
            //result.SupplyTime = businessHours;
            //result.Products = products.ToList();
            //result.productCategories = productCategories.ToList();
            result.Score1 = _repo.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => (decimal?)x.Score1) == null ? 0 : _repo.GetAll<Score>().Where(x => x.ScoreId == store.StoreId).Average(x => x.Score1);

            result.StoreCategoryName = _repo.GetAll<StoreCategory>().First(x => x.StoreCategoryId == store.StoreCategoryId).Name;
            return result;
        }

    }
}