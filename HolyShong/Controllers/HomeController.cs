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

        public ActionResult StoreCategorySearch(int? id)
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

        //[HttpGet]
        //public ActionResult Search()
        //{
        //    return View();
        //}

        //[HttpPost]
        public ActionResult Search()
        {
            var result = _storeService.GetAllStoresByKeyword();
            //轉換其它頁面
            return View(result);
        }


   
    }
}