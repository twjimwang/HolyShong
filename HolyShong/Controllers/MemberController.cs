using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Models;
using Newtonsoft.Json;
using HolyShong.Services;

namespace HolyShong.Controllers
{
    public class MemberController : Controller
    {
        public CartService _cartService;
        public MemberController()
        {
            _cartService = new CartService();
        }
   

        // GET: Member
        public ActionResult Index()
        {
            ViewBag.xtest = _cartService.GetCartViewModels().First();
            return View(ViewBag.xtest);
            //return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Checkout()
        {
            ViewBag.xtest = _cartService.GetCartViewModels().First();
            return View();
        }

        //public ActionResult CheckoutTest()
        //{
        //    var cart = _ctx.Carts.ToList();
        //    string jsonCart = JsonConvert.SerializeObject(cart);
        //    return Content(jsonCart);
        //}


        public ActionResult Eatpass()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult DeliverRegister()
        {
            return View();
        }
    }
}