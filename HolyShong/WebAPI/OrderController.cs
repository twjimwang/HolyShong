using HolyShong.Services;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HolyShong.WebAPI
{

    public class OrderController : ApiController
    {
        private readonly OrderService _orderService;
        public OrderController()
        {
            _orderService = new OrderService();
        }

        [HttpPost]
        public IHttpActionResult FormFromCheckOut(CheckOutViewModel checkoutVM)
        {
            //抓member
            var memberId = Int32.Parse(User.Identity.Name);
            var session = HttpContext.Current.Session; //宣告Session
                                                       
            var cart = (List<StoreProduct>)session["Cart"];//抓session

            //成立訂單
            var order = _orderService.OrderCreate(memberId, cart, checkoutVM);
            return Ok();
        }
    }
}
