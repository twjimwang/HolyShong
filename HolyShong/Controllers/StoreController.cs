using HolyShong.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolyShong.Controllers
{
    public class StoreController : Controller
    {
        private readonly ProductService _productService;
        private readonly MarketService _marketService;
        public StoreController()
        {
            _productService = new ProductService();
            _marketService = new MarketService();
        }
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Restaurant(int? id)
        {
            #region
            //RestaurantVM result = new RestaurantVM();
            //var store = _ctx.Store.FirstOrDefault((x) => x.StoreId == id);
            //if (store == null)
            //{
            //    return RedirectToAction("NoSearch", "Home");
            //}
            //var productCategories = _ctx.ProductCategory.Where((x) => x.StoreId == store.StoreId);

            //var products = _ctx.Product.Where(x => productCategories.Select(y => y.ProductCategoryId).Contains(x.ProductCategoryId));

            //result.StoreId = store.StoreId;
            //result.StoreName = store.Name;
            //result.StorePicture = store.Img;
            //result.StoreAddress = store.Address;
            //result.Products = products.ToList();
            //result.productCategories = productCategories.ToList();
            //result.Score1 = _ctx.Score.Where(x => x.ScoreId == store.StoreId).Average(x => x.Score1);
            //result.StoreCategoryName = _ctx.StoreCategory.First(x => x.StoreCategoryId == store.StoreCategoryId).Name;
            #endregion

            if (!id.HasValue)
            {
                return RedirectToAction("NoSearch", "Home");
            }
            //var result = _restaurantService.GetRestaurant(id);
            var result = _productService.GetStore(id.Value);
            if (result.StoreName == null)
            {
                return RedirectToAction("NoSearch", "Home");
            }
            return View(result);

        }
        //測試
        //public ActionResult RestaurantTEST(int? storeid)
        //{
        //    if (!storeid.HasValue)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    var result =_productService.GetAllProductsByStoreId(storeid.Value);
        //    //if (result.StoreName == null)
        //    //{
        //    //    return RedirectToAction("NoSearch", "Home");
        //    //}
        //    return View(result);

        //}


        public ActionResult Marketing(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("NoSearch", "Home");
            }
            var result = _marketService.GetEventRestaurant(id.Value);
            if (result.eventRestaurants == null)
            {
                return RedirectToAction("NoSearch", "Home");
            }
            return View(result);
        }
    }
}