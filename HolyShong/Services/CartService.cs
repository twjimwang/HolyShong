using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class CartService
    {
        private readonly HolyShongRepository _repo;
        public CartService()
        {
            _repo = new HolyShongRepository();
        }

        //public void AddCart(StoreProduct storeProductVM)
        //{
        //    var product = _repo.GetAll<Product>().FirstOrDefault(p => p.ProductId == storeProductVM.ProductId);
        //    var productCategory = _repo.GetAll<ProductCategory>().FirstOrDefault(pc => pc.ProductCategoryId == product.ProductCategoryId);
        //    var store = _repo.GetAll<Store>().Where(s => s.StoreId == productCategory.StoreId);
        //    var productOption = _repo.GetAll<ProductOption>().Where(po => po.ProductId == product.ProductId);
        //}

        //public void ShowCart(StoreProduct storeProductVM)
        //{
        //    var product = _repo.GetAll<Product>().FirstOrDefault(p => p.ProductId == storeProductVM.ProductId);
        //    var productCategory = _repo.GetAll<ProductCategory>().FirstOrDefault(pc => pc.ProductCategoryId == product.ProductCategoryId);
        //    var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == productCategory.StoreId);

        //    組成購物車
        //    var ShoppingCart = new CartViewModel()
        //    {
        //        IsPlasticbag = false,
        //        IsTablewares = false,
        //        StoreId = store.StoreId,
        //        StoreName = store.Name,
        //        CartItems = storeProductVM
        //    };


        //    加入資料庫
        //}
    }
}