using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Models.HolyShongModel;
using HolyShong.Services;
using HolyShong.ViewModels;

namespace HolyShong.Controllers
{
    public class HomeController : Controller
    {
        //初始
        private readonly StoreCategoryService _storecategoryService;
        private readonly StoreService _storeService;

        public HomeController()
        {
            _storecategoryService = new StoreCategoryService();
            _storeService = new StoreService();
        }


        public ActionResult Index()
        {

            var result = new HomeViewModel()
            {
                StoreCategories = new List<ViewModels.StoreCategory>()
            };
            
            result = _storeService.GetAllStores();
            result.StoreCategories = _storecategoryService.GetAllStoreCategories();
            return View(result);
        }

        public ActionResult Search(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("NoSearch");
            }
            var result = _storeService.GetAllStoresByStoreCategoryId(id.Value); 
            return View(result);
        }

        public ActionResult NoSearch()
        {
            return View();
        }







        //內建預設View
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

     
    }
}