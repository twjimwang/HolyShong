using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;

namespace HolyShong.Services
{
    public class ProductService
    {
        //初始
        private readonly HolyShongRepository _storecategoryRespository;
        private readonly HolyShongRepository _storeRespository;
        private readonly HolyShongRepository _productcategoryRespository;
        private readonly HolyShongRepository _productRespository;

        public ProductService()
        {
            _storecategoryRespository = new HolyShongRepository();
            _storeRespository = new HolyShongRepository();
            _productcategoryRespository = new HolyShongRepository();
            _productRespository = new HolyShongRepository();
        }
        //店家卡片

        public RestaurantViewModel GetAllProductsByStoreId(int? storeid)
        {
            var result = new RestaurantViewModel
            {
                ProductAreas = new List<ProductArea>()
            };


            //1.找出所有產品類別(不用做)
            var productcategories = _productcategoryRespository.GetAll<ProductCategory>().Where(x => x.StoreId == storeid).ToList();
            //2.找出所有類別下面的所有產品
            var products = _productRespository.GetAll<Product>().ToList();
            //3.依產品類別將產品分類後再全部取出
            foreach (var item in productcategories)
            {
                //這個產品類別(ProductsCategory)的產品(Product)全部挑出來
                var temp = products.Where(x => x.ProductCategoryId == item.ProductCategoryId);
                //存成ProductCards
                var cards = new List<ProductCard>();


                foreach (var product in temp)
                {
                    var card = new ProductCard
                    {
                        ProductName = product.Name,
                        Img = product.Img,
                        UnitPrice = product.UnitPrice,
                        Description = product.Description
                        //StoreId = store.StoreId,
                        //StoreImg = store.Img,
                        //StoreName = store.Name
                    };
                    cards.Add(card);
                }
                var area = new ProductArea
                {
                    ProductCategoryName = item.Name,
                    ProductCards = cards

                    //StoreCategoryId = item.StoreCategoryId,
                    //StoreCategoryImg = item.Img,
                    //StoreCategoryName = item.Name,
                    //StoreCards = cards
                };
                result.ProductAreas.Add(area);
            }
            return result;
        }








    }
}