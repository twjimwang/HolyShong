using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Models;
using Newtonsoft.Json;
using HolyShong.Services;
using System.Net;
using HolyShong.ViewModels;

namespace HolyShong.Controllers
{
    public class MemberController : Controller
    {
        public CartService _cartService;
        public HolyCartViewModel _HolyCartViewModel; 

        public MemberController()
        {
            _cartService = new CartService();
            _HolyCartViewModel = new HolyCartViewModel();
        }
   

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Checkout()
        {   
            //if(Id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            // ViewBag.xtest = _cartService.GetCartViewModels().First();

            var model = _cartService.GetCartByMemberId(1);
            // ViewBag.Count = new SelectList(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            return View();
        }
        //[HttpPost]
        //public ActionResult Checkout()
        //{
        //    //if(Id == null)
        //    //    {
        //    //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //    }
        //    // ViewBag.xtest = _cartService.GetCartViewModels().First();

        //    var model = _cartService.GetCartByMemberId(1);
        //    // ViewBag.Count = new SelectList(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        //    return View();
        //}


        public ActionResult Eatpass()
        {
            return View();
        }
        public ActionResult UserProfile()
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

        public ActionResult Favorite()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult RestaurantRegister()
        {
            return View();
        }

        public ActionResult Invite()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}