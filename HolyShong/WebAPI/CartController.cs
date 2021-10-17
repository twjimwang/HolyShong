using HolyShong.Models.HolyShongModel;
using HolyShong.Services;
using HolyShong.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HolyShong.WebAPI
{

    public class CartController : ApiController
    {
        private readonly CartService _cartService;
        public CartController()
        {
            _cartService = new CartService();
        }


        [HttpPost]
        public IHttpActionResult AddToCart(StoreProduct storeProductVM)
        {
            List<StoreProduct> cartList = new List<StoreProduct>();
            var session = HttpContext.Current.Session; //宣告Session
   
            if (session["Cart"] == null)
            {
                cartList.Add(storeProductVM);
                session["Cart"] = cartList;
            }
            else
            {
                //不同店家，清除購物車
                cartList = (List<StoreProduct>)session["Cart"];
                cartList.Add(storeProductVM);
                session["Cart"] = cartList;
            }

            return Ok(cartList);
        }


        [HttpPost]
        public IHttpActionResult ShowCart()
        {

            var session = HttpContext.Current.Session;
            var cartList = (List<StoreProduct>)session["Cart"] == null ? new List<StoreProduct>(): (List<StoreProduct>)session["Cart"];          

            return Ok(cartList);
        }

        [HttpPost]
        public IHttpActionResult DeleteCartItem(List<StoreProduct> storeProductVMs)
        {
            var session = HttpContext.Current.Session;
            session.Remove("Cart");
            session["Cart"] = storeProductVMs;

            return Ok();
        }
    }
}
