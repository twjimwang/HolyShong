using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Services;

namespace HolyShong.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly test _testService;
        public OrderController()
        {
            _orderService = new OrderService();
            _testService = new test();
        }
        // GET: Order
        /// <summary>
        /// 消費者外送頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Checkout","Member");
            }
            var result = _orderService.GetOrder(id.Value);
            return View(result);
        }

        public ActionResult Test()
        {
            var result = _testService.GetProducts();
            return View(result);
        }
    }
}