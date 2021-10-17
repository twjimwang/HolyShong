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
    [RoutePrefix("api/[Controller]/[Action]")]
    //屬性路由
    //可以拿postman測試
    public class PaymentController : ApiController
    {
        private readonly PayServices _payServices;

        public PaymentController()
        {
            _payServices = new PayServices();
        }
        //從綠界接收資料後，回傳"1|OK"
        [HttpPost]
        //[Route("api/payment/GetResultFromECPay")]
        public IHttpActionResult GetResultFromECPay(GetResultFromECPay query)
        {
            //付款成功
            if (query.RtnCode == 1)
            {
                _payServices.BecomVIPSuccess(int.Parse(query.CustomField1));
            }
            return Ok("1|OK");
        }

    }
}
