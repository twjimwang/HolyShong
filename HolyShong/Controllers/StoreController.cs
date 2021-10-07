using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolyShong.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Restaurant()
        {
            return View();
        }
        public ActionResult Marketing()
        {
            return View();
        }
        public ActionResult Store()
        {
            return View();
        }
        public ActionResult BusinessHours()
        {
            return View();
        }
    }
}