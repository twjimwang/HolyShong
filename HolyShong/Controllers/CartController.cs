using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Services;
using HolyShong.ViewModels;
using HolyShong.Models;
using System.Net;

namespace HolyShong.Controllers
{
    public class CartController : Controller
    {
        public CartService _cartService;
        public HolyCartViewModel _HolyCartViewModel;

        public CartController()
        {
            _cartService = new CartService();
            _HolyCartViewModel = new HolyCartViewModel();
           

        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cart()
        {
            var holyCartViewmodel = _cartService.GetCartByMemberId(1);
            TempData["holyCart"] = holyCartViewmodel;


            return View();
        }

        // 找到 MemberId
        public ActionResult FindCartMemberId(int memberId)
        {
           
            if (memberId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var holyCartViewModel = _cartService.GetCartByMemberId(1);
            
            if(holyCartViewModel == null)
            {
                return HttpNotFound();
            }

            HolyCartViewModel holyCartVM = new HolyCartViewModel
            {
                CartId = holyCartViewModel.CartId,
                MemberId = holyCartViewModel.MemberId,
                StoreName = holyCartViewModel.StoreName,
                CartItems = holyCartViewModel.CartItems,
                Address = holyCartViewModel.Address,





            };


            // 下拉選單
            // var cartList = new List<SelectListItem>();


            ViewBag.Count = new SelectList(new[] { 1, 2, 3, 4, 5 });


            //ViewBag.Count = cartList;
            return View(holyCartVM);
        }


        // 購物車資料存到 SESSION
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
                    MemberId = holyCartViewModelVM.MemberId,
                    StoreName = holyCartViewModelVM.StoreName,
                    CartItems = holyCartViewModelVM.CartItems,
                    // Address = holyCartViewModelVM.Address,
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
                    MemberId = holyCartViewModelVM.MemberId,
                    StoreName = holyCartViewModelVM.StoreName,
                    CartItems = holyCartViewModelVM.CartItems,
                    // Address = holyCartViewModelVM.Address,
                    Count = holyCartViewModelVM.Count + 1,


                };
                holyCartViewModels.Add(holyCartViewModel);
                Session["HolyCartViewModel"] = holyCartViewModels;
            }

            return holyCartViewModels.Count;
        }
        // 從SESSION還原購物車資料
        public ActionResult ListCart()
        {
            List<HolyCartViewModel> holyCartViewModels = (List<HolyCartViewModel>)Session["HolyCartViewModel"];
            return View(holyCartViewModels);
        }
    }
}