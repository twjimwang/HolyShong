using HolyShong.Models;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Repositories;


namespace HolyShong.Controllers
{
    public class MemberController : Controller
    {
        private readonly HolyShongRepository _repo;

        public MemberController()
        {
            _repo = new HolyShongRepository();
        }
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Login()
        //{
        //    return View();
        //}

        public ActionResult Checkout()
        {
            return View();
        }

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


        /// <summary>
        /// 登入頁面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberRegistrViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = _repo.GetAll<>.SingleOrDefault(x => x.Name.ToUpper() == loginVM.Email.ToUpper()
                && x.Password == loginVM.Password);

                if (user != null)
                {
                    return Content($"使用者:{user.Name}你好");
                }
                else
                {
                    return Content("找不到使用者");
                }

            }
            return View(loginVM);

        }
    }
}