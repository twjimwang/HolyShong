using HolyShong.Services;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolyShong.Controllers
{
    public class DeliverController : Controller
    {
        private readonly OrderService _orderService;
        public DeliverController()
        {
            _orderService = new OrderService();
        }
        // GET: Deliver
        public ActionResult Index()
        {
            var result = _orderService.GetOrderForDeliver();
            return View(result);
        }

        /// <summary>
        /// 外送員上下線
        /// </summary>
        /// <param name="deliverConnectionVM"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeliverConnectionStatus(DeliverConnectionViewModel deliverConnectionVM)
        {

            var result = _orderService.GetDeliverConnection(deliverConnectionVM.isOnline);

            return result;
        }



    }
}