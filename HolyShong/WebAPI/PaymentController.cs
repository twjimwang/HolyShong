using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HolyShong.Services;

namespace HolyShong.WebAPI
{
    //[RoutePrefix("api/[Controller]/[Action]")]
    //設定屬性路由
    //可以拿postman測試
    public class PaymentController : ApiController
    {
        private readonly PayServices _payServices;

        public PaymentController()
        {
            _payServices = new PayServices();
        }

        /// <summary>
        /// 升級送送會員從綠界接收資料後，回傳通知綠屆"1|OK"
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/payment/VIPGetResultFromECPay")]
        public IHttpActionResult VIPGetResultFromECPay(GetResultFromECPay query)
        {          
            
            //付款成功
            if (query.RtnCode == 1)
            {
                _payServices.BecomVIPSuccess(int.Parse(query.CustomField1));
            }
            //回報綠界
            return Ok("1|OK");
        }

        /// <summary>
        /// 購買購物車內商品從綠界接收資料後，回傳通知綠屆"1|OK"
        /// </summary>
        /// <param name = "query" ></ param >
        /// < returns ></ returns >
        [HttpPost]
        [Route("api/payment/BuyCartGetResultFromECPay")]
        public IHttpActionResult BuyCartGetResultFromECPay(GetResultFromECPay query)
        {
            

            //付款成功
            if (query.RtnCode == 1)
            {
                _payServices.BuyCartIsSuccess(int.Parse(query.CustomField1));
            }
            //回報綠界
            return Ok("1|OK");
        }

        /// <summary>
        /// 給外送員小費從綠界接收資料後，回傳通知綠屆"1|OK"
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("api/payment/FeeGetResultFromECPay")]
        public IHttpActionResult FeeGetResultFromECPay(GetResultFromECPay query)
        {
            

            //付款成功
            if (query.RtnCode == 1)
            {
                _payServices.BecomVIPSuccess(int.Parse(query.CustomField1));
            }
            //回報綠界
            return Ok("1|OK");
}
    }
}
