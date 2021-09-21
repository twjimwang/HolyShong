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
        public OrderService _service;
        public OrderController()
        {
            _service = new OrderService();
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderDeliver()
        {
            var odlist = _service.GetOrderVM().FirstOrDefault();

            return View(odlist);
        }
    }
}