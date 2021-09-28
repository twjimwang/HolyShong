using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Models.HolyShongModel;
using HolyShong.Services;

namespace HolyShong.Controllers
{
    public class HomeController : Controller
    {
        //初始
        private readonly StoreCategoryService _storecategoryService;
        public HomeController()
        {
            _storecategoryService = new StoreCategoryService();
        }


        public ActionResult Index()
        {
            var result = _storecategoryService.GetAllStoreCategories();
            return View(result);
        }

        public ActionResult Search()
        {
            return View();
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