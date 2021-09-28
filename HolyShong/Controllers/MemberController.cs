using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolyShong.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Eatpass()
        {
            return View();
        }
        /// <summary>
        /// 會員個人資料頁面
        /// 讀取資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Profile(int? id)
        {
            if (!id.HasValue)
            {

            }
            return View();
        }
        /// <summary>
        /// 會員個人資料頁面
        /// 修改資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Profile(MemberProfileViewModel memberProfileViewModel)
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
        /// <summary>
        /// 註冊頁面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(MemberRegistrViewModel registerViewModel)
        {
            return View();
        }
    }
}