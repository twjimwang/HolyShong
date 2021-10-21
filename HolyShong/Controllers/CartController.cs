using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolyShong.Controllers
{
    public class CartController : Controller
    {
        //確認是否登陸
        public int IsLogin()
        {
            var a = User.Identity.Name;
            if (User.Identity.Name == "")
            {
                return 1; //未登入
            }
            else
            {
                return 2; //已登入
            }
        }


        //
        public ActionResult ShowCheckOut()
        {
            return View();
        }
    }
}