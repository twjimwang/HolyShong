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
        public OrderController()
        {
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

    }
}