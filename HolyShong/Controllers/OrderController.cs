using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Services;
using HolyShong.ViewModels;

namespace HolyShong.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController()
        {
            _orderService = new OrderService();
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
    }
}