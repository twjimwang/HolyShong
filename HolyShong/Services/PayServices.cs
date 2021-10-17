using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;
using System.Data.Entity;
using HolyShong.Repositories;

namespace HolyShong.Services
{
    public class PayServices
    {
        private readonly HolyShongRepository _repo;

        public PayServices()
        {
            _repo = new HolyShongRepository();
        }

        /// <summary>
        /// 升級送送會員
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string BecomeVIPService(int id)
        {
            var becomVIPObject = new ECPayViewModel()
            {
                MemberId = id,
                ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5",//付款完成通知回傳的網址
                ClientBackURL = "http://localhost:44360/Member/UserProfile",//瀏覽器端返回的廠商網址
                OrderResultURL = "",//瀏覽器端回傳付款結果網址
                MerchantTradeNo = "",//廠商的交易編號
                TotalAmount = 0,//交易總金額
                ListItems = new List<BuyItem>
                {
                  new BuyItem
                  {
                      Name="升級送送會員",
                      Currency="新臺幣",
                      Price=30,
                      Quantity=1,
                      URL="",
                  }
                },
            };

            string html = ECPayBasic(becomVIPObject);
            return html;
        }
        public string ECPayBasic(ECPayViewModel eCPayViewModel)
        {
            AllInOne oPayment = new AllInOne();
            /* 服務參數 */
            oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法            
            oPayment.ServiceURL = eCPayViewModel.ServiceURL;//要呼叫介接服務的網址
            oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
            oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
            oPayment.MerchantID = "2000132";//ECPay提供的特店編號

            /* 基本參數 */
            oPayment.Send.ReturnURL = "https://7a3d-1-164-253-196.ngrok.io/api/payment/GetResultFromECPay";//付款完成通知回傳的網址，測試版隨ngrok調整
            oPayment.Send.ClientBackURL = eCPayViewModel.ClientBackURL;//瀏覽器端返回的廠商網址
            oPayment.Send.OrderResultURL = "";//瀏覽器端回傳付款結果網址
            oPayment.Send.MerchantTradeNo = "HolyShong" + new Random().Next(0, 99999).ToString();//廠商的交易編號
            oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
            oPayment.Send.TotalAmount = Decimal.Parse("30");//交易總金額
            oPayment.Send.TradeDesc = "交易描述";//交易描述
            oPayment.Send.ChoosePayment = PaymentMethod.Credit;//使用的付款方式
            oPayment.Send.Remark = "";//備註欄位
            oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
            oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.No;//是否需要額外的付款資訊
            oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
            oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
            oPayment.Send.PlatformID = "";//特約合作平台商代號
            oPayment.Send.HoldTradeAMT = HoldTradeType.Yes;
            oPayment.Send.CustomField1 = eCPayViewModel.MemberId.ToString();//備註欄，會員Id
            oPayment.Send.CustomField2 = "";
            oPayment.Send.CustomField3 = "";
            oPayment.Send.CustomField4 = "";
            oPayment.Send.EncryptType = 1;

            //訂單的商品資料
            var buyitems = eCPayViewModel.ListItems;
            foreach (var buyitem in buyitems)
            {
                oPayment.Send.Items.Add(new ECPay.Payment.Integration.Item()
                {
                    Name = "升級送送會員",//商品名稱
                    Price = Decimal.Parse("30"),//商品單價
                    Currency = "新台幣",//幣別單位
                    Quantity = Int32.Parse("1"),//購買數量
                    URL = "",//商品的說明網址
                });
            }


            var html = string.Empty;
            oPayment.CheckOutString(ref html);
            return html;
        }

        public void BecomVIPSuccess(int id)
        {
            //
            var rank = new Rank()
            {
                RankId = 28,
                MemberId = id,
                IsPrimary = true,
                EndTime = DateTime.UtcNow.AddDays(30),
            };
            DbContext context = new HolyShongContext();
            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    _repo.Create<Rank>(rank);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }

        }
    }
}