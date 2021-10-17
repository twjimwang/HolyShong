using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Services;
using HolyShong.ViewModels;

namespace HolyShong.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PayServices _payServices;

        public PaymentController()
        {
            _payServices = new PayServices();
        }

        // GET: ECPay
        [Authorize]
        public ActionResult BecomeVIP()
        {          
            var html = _payServices.BecomeVIPService(int.Parse(User.Identity.Name));
            ViewBag.Html = html;
            return View();
        }

        [Authorize]
        public ActionResult PayForCart()
        {
            //var html = _payServices.BecomeVIP(int.Parse(User.Identity.Name));
            //ViewBag.Html = html;
            return View();
        }

        [Authorize]
        public ActionResult DeliverFee()
        {
            //var html = _payServices.BecomeVIP(int.Parse(User.Identity.Name));
            //ViewBag.Html = html;
            return View();
        }

        //[HttpPost]
        //public ActionResult GetResultFromECPay(GetResultFromECPay query)
        //{

        //    return View();
        //}
    }
}