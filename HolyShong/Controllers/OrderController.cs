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
        private readonly test _service;
        public OrderController()
        {
            _service = new test();
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderDeliver()
        {
            return View();
        }

        public ActionResult Test()
        {
            var result = _service.GetProducts();
            return View(result);
        }
    }
}