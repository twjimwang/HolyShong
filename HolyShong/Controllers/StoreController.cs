using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Models;
using HolyShong.ViewModels;
using Newtonsoft.Json;
namespace HolyShong.Controllers
{
    public class StoreController : Controller
    {
        public StoreContext _ctx;
        public StoreController()
        {
            _ctx = new StoreContext();
        }
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Restaurant()
        {
            //DB撈資料
            var Store = _ctx.Stores.ToList();
            var ProductCategory = _ctx.ProductCategories.ToList();
            var Products = _ctx.Products.ToList();
            var Score = _ctx.Scores.ToList();
            var StoreCategory = _ctx.StoreCategory.ToList();
            var Product =
                 from s in Store
                 join pc in ProductCategory on s.StoreId equals pc.StoreId
                 join p in Products on pc.ProductCategoryID equals p.ProductCategoryId
                 where s.StoreId == 2 && pc.ProductCategoryID == 1
                 select new Product
                 {
                     Name = p.Name,
                     Img = p.Img,
                     UnitPrice = p.UnitPrice

                 };

            var RestaurantVM =
                          from s in Store
                          join pc in ProductCategory on s.StoreId equals pc.StoreId
                          join p in Products on pc.ProductCategoryID equals p.ProductCategoryId
                          join sc in Score on s.StoreId equals sc.StoreId
                          join stc in StoreCategory on s.StoreCategoryId equals stc.StoreCategoryId
                          where s.StoreId==2
                          select new RestaurantVM //把db的資料給RestaurantVM
                          {
                              ProductCategoryID = pc.ProductCategoryID,
                              PcStoreId = pc.StoreId,                 
                              ProductCategorySort = pc.Sort,         
                              ProductCategoryName = pc.Name,
                              SupplyTime=pc.SupplyTime,
                              StoreCategoryId = s.StoreCategoryId,                           
                              StoreName = s.Name,
                              StorePicture = s.Picture,
                              StoreAddress = s.Address,
                              Products = Product.ToList(), //整筆資料
                              Score1=sc.Score1,
                              StoreCategoryName=stc.Name,                   
                              PcgName=ProductCategory
                          };
         

            return View(RestaurantVM.First());
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