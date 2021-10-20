﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Services;
using HolyShong.ViewModels;

namespace HolyShong.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        public OrderController()
        {
            _orderService = new OrderService();
        }
        // GET: Order
        /// <summary>
        /// 等綠界回覆，消費者外送頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            //抓完成訂單
            //刪除session


            return View();
        }


        // GET: Order
        /// <summary>
        /// 歷史訂單導向消費者外送頁
        /// </summary>
        /// <returns></returns>
        public ActionResult UndoneOrderList(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            var result = _orderService.GetOrder(id.Value);
            return View("Index",result);
        }
    }
}