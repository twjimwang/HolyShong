using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Services;

namespace HolyShong.Controllers
{
    public class MemberController : Controller
    {
        public ProfileService _memberProfileVM;
        public MemberController()
        {
            _memberProfileVM = new ProfileService();
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
            var memberProfileVM = _memberProfileVM.GetMemberProfileData().First();
            return View(memberProfileVM);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }


    }
}