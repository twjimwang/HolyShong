using HolyShong.Services;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HolyShong.WebAPIControllers
{

    public class CartController : ApiController
    {
        private readonly OrderService _orderService;
        public CartController()
        {
            _orderService = new OrderService();
        }


        [HttpPost]
        public IHttpActionResult AddToCart(StoreProduct storeProductVM)
        {

            return Ok();
        }
    }
}
