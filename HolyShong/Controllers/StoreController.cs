using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HolyShong.Models;
using HolyShong.Models.HolyShongModel;
using HolyShong.ViewModels;
using Newtonsoft.Json;
namespace HolyShong.Controllers
{
    public class StoreController : Controller
    {
        public HolyShongContext _ctx;
        public StoreController()
        {
            _ctx = new HolyShongContext();
        }
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Restaurant(int id = 0)
        {
            RestaurantVM result = new RestaurantVM();
            var store = _ctx.Store.FirstOrDefault((x) => x.StoreId == id);
            if (store == null)
            {
                return RedirectToAction("NoSearch", "Home");
            }
            var productCategories = _ctx.ProductCategory.Where((x) => x.StoreId == store.StoreId);

            var products = _ctx.Product.Where(x => productCategories.Select(y => y.ProductCategoryId).Contains(x.ProductCategoryId));

            result.StoreId = store.StoreId;
            result.StoreName = store.Name;
            result.StorePicture = store.Img;
            result.StoreAddress = store.Address;
            result.Products = products.ToList();
            result.productCategories = productCategories.ToList();
            result.Score1 = _ctx.Score.Where(x => x.ScoreId == store.StoreId).Average(x => x.Score1);
            result.StoreCategoryName = _ctx.StoreCategory.First(x => x.StoreCategoryId == store.StoreCategoryId).Name;


            return View(result);

        }
        //public ActionResult Test()
        //{
        //    var stores = _ctx.Stores.ToList();
        //    string Jsonstores = JsonConvert.SerializeObject(stores, new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //        PreserveReferencesHandling = PreserveReferencesHandling.Objects
        //    });
        //    ViewBag.Jsonstores = Jsonstores;
        //    return Content(Jsonstores);
        //}
        public ActionResult Marketing()
        {
            return View();
        }
        public ActionResult RestaurantRegister()
        {
            return View();
        }
    }
}