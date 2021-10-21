using HolyShong.Services;
using HolyShong.ViewModels;
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
        /// <summary>
        /// 餐廳頁面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Restaurant(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("NoSearch", "Home");
            }        
            var result = _productService.GetStore(id.Value);
            if (result.StoreName == null)
            {
                return RedirectToAction("NoSearch", "Home");
            }
            return View(result);

        }      
        /// <summary>
        /// 活動店家頁面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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