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
        ////顯示購物車
        //public ActionResult ShowCart()
        //{

        //}


        //
        public ActionResult ShowCheckOut()
        {
            return View();
        }
    }
}