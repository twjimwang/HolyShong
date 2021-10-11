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

        public ActionResult Cart() 
        {
            var holyCartViewmodel = _cartService.GetCartByMemberId(1);
            TempData["holyCart"] = holyCartViewmodel;

       
            return View();
        }

        [HttpPost]
        public int AddToHolyCart(HolyCartViewModel holyCartViewModelVM)
        {
            List<HolyCartViewModel> holyCartViewModels = new List<HolyCartViewModel>();
            if (Session["HolyCartViewModel"] == null)
            {
                HolyCartViewModel holyCartViewModel = new HolyCartViewModel
                {
                    RecordId = 1,
                    CartId = holyCartViewModelVM.CartId,
                    StoreName = holyCartViewModelVM.StoreName,
                    CartItems = holyCartViewModelVM.CartItems,
                    Address = holyCartViewModelVM.Address,
                    Count = holyCartViewModelVM.Count
                };
                holyCartViewModels.Add(holyCartViewModel);
                Session["HolyCartViewMode;"] = holyCartViewModels;
            }
            else
            {
                holyCartViewModels = (List<HolyCartViewModel>)Session["HolyCartViewModel"];
                HolyCartViewModel holyCartViewModel = new HolyCartViewModel
                {
                    RecordId = holyCartViewModels.Count() + 1,
                    CartId = holyCartViewModelVM.CartId,
                    StoreName = holyCartViewModelVM.StoreName,
                    CartItems = holyCartViewModelVM.CartItems,
                    Address = holyCartViewModelVM.Address,
                    Count = holyCartViewModelVM.Count,


                };
                holyCartViewModels.Add(holyCartViewModel);
                Session["HolyCartViewModel"] = holyCartViewModels;
            }

            return holyCartViewModels.Count;
        }


        public ActionResult Checkout(int? Id)
        {   //if(Id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            // ViewBag.xtest = _cartService.GetCartViewModels().First();
            var model = _cartService.GetCartByMemberId(1);

            ViewBag.Count = new SelectList(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            return View();
        }

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