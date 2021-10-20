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
        private readonly OrderService _orderService;

        public PaymentController()
        {
            _payServices = new PayServices();
            _orderService = new OrderService();
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
        public ActionResult PayForCart(CheckOutViewModel checkoutVM)
        {
            //測試訂單
            var memberId = Int32.Parse(User.Identity.Name);
            var cart = (List<StoreProduct>)Session["Cart"];//抓session

            //成立訂單
            var order = _orderService.OrderCreate(memberId, cart, checkoutVM);


            //var html = _payServices.BuyCartService(int.Parse(User.Identity.Name));
            //ViewBag.Html = html;
            return View();
        }

        //Todo 有時間再做付小費
        //[Authorize]
        //public ActionResult DeliverFee()
        //{
        //    var html = _payServices.BecomeVIP(int.Parse(User.Identity.Name));
        //    ViewBag.Html = html;
        //    return View();
        //}

    }
}