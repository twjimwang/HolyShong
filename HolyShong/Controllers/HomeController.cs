using HolyShong.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Models.HolyShongModel;
using HolyShong.ViewModels;

namespace HolyShong.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiscountService _discountService;
        private readonly StoreCategoryService _storecategoryService;
        private readonly StoreService _storeService;
        public HomeController()
        {
            _discountService = new DiscountService();
            _storecategoryService = new StoreCategoryService();
            _storeService = new StoreService();
        }
        //初始

        public ActionResult Index()//首頁
        {
            var result = new HomeViewModel()
            {
                StoreCategories = new List<HomeStoreCategory>(),
                NumberArray = new int[5]
            };

            result = _storeService.GetAllStores();
            result.StoreCategories = _storecategoryService.GetAllStoreCategories();
            result.NumberArray = _storecategoryService.GetRandomNumber();
            return View(result);
        }

        public ActionResult StoreCategorySearch(int? id)//主分類搜尋
        {
            if (!id.HasValue)
            {
                return RedirectToAction("NoSearch");
            }
            var result = _storeService.GetAllStoresByStoreCategoryId(id.Value);
            return View(result);
        }

        public ActionResult NoSearch()//搜尋不到頁面
        {
            return View();
        }

        //test
        [HttpGet]
        public ActionResult Search()
        {

            var result = _storeService.GetAllStoresByKeyword("");
            return View(result);
        }

        [HttpPost]
        public ActionResult Search(string keyword)//搜尋頁面
        {
            var result = _storeService.GetAllStoresByKeyword(keyword);
            ViewBag.keyword = keyword;
            //轉換其它頁面
            if (result.StoreCards.Count==0)
            {
                return View("NoSearch"); 
            }

            ViewBag.searchCount = result.StoreCards.Count;

            return View(result);
        }

        public ActionResult SubCategorySearchByPrice()//副分類搜尋頁面
        {
            var result = _storeService.GetAllStoresByKeyword("康");
            //轉換其它頁面
            return View(result);
        }

  

        public void AcquireDiscount(string discountName)
        {
            _discountService.AcquireDiscount(discountName);
        }
    }
}