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
        public ActionResult Index(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            var result = _orderService.GetOrderForDeliver(id.Value);
            return View(result);
        }

        [HttpPost]
        public bool DeliverConnectionStatus(DeliverConnectionViewModel deliverConnectionVM)
        {

            var result = _orderService.GetDeliverConnection(deliverConnectionVM.isOnline);

            return result;
        }
    }
}