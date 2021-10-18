using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;
using System.Data.Entity;
using HolyShong.Repositories;
using Item = HolyShong.Models.HolyShongModel.Item;

namespace HolyShong.Services
{
    public class PayServices
    {
        private readonly HolyShongRepository _repo;

        public PayServices()
        {
            _repo = new HolyShongRepository();
        }

        //設定回傳綠界api路徑
        string VIPReturnURL = "https://bd30-2001-b011-3800-32dc-952f-89e8-beae-d024.ngrok.io/api/payment/GetResultFromECPay";
        string cartReturnURL = "https://bd30-2001-b011-3800-32dc-952f-89e8-beae-d024.ngrok.io/api/payment/GetResultFromECPay";


        /// <summary>
        /// 購買購物車內商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string BuyCart(int id)
        {
            var cart = _repo.GetAll<Cart>().First(x => x.MemberId == id);
            var itemsInCart = _repo.GetAll<Item>().Where(x => x.CartId == cart.CartId);
            var CartDetail = new ECPayViewModel()
            {
                MemberId = id,
                ReturnURL = cartReturnURL,
                ClientBackURL = "http://localhost:44360",
                OrderResultURL = "",
                MerchantTradeNo = "",
                TotalAmount = 0,
                ListItems = new List<BuyItem> { },
            };

            foreach (var itemInCart in itemsInCart)
            {
                var tempItem = new BuyItem
                {
                    Name = itemInCart.Product.Name,
                    Currency = "新臺幣",
                    Price = itemInCart.Product.UnitPrice,
                    Quantity = itemInCart.Quantity,
                    URL = "",
                };
                CartDetail.ListItems.Add(tempItem);


                //總金額
                CartDetail.TotalAmount += tempItem.Price;
            };



            string html = ECPayBasic(CartDetail);
            return html;
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
                ClientBackURL = "http://localhost:44360/Member/UserProfile",//瀏覽器端返回的廠商網址
                ReturnURL = VIPReturnURL,
                OrderResultURL = "",//瀏覽器端回傳付款結果網址
                MerchantTradeNo = "",//廠商的交易編號
                TotalAmount = 30,//交易總金額
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


        /// <summary>
        /// 綠界SDK
        /// </summary>
        /// <param name="eCPayViewModel"></param>
        /// <returns></returns>
        public string ECPayBasic(ECPayViewModel eCPayViewModel)
        {
            AllInOne oPayment = new AllInOne();
            /* 服務參數 */
            oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法            
            oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
            oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
            oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
            oPayment.MerchantID = "2000132";//ECPay提供的特店編號

            /* 基本參數 */
            oPayment.Send.ReturnURL = eCPayViewModel.ReturnURL;//付款完成通知回傳的網址，測試版隨ngrok調整
            oPayment.Send.ClientBackURL = eCPayViewModel.ClientBackURL;//瀏覽器端返回的廠商網址
            oPayment.Send.OrderResultURL = "";//瀏覽器端回傳付款結果網址
            oPayment.Send.MerchantTradeNo = "HolyShong" + new Random().Next(0, 99999).ToString();//廠商的交易編號
            oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
            oPayment.Send.TotalAmount = eCPayViewModel.TotalAmount;//交易總金額
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
            oPayment.Send.CustomField2 = "";//備註欄，訂單編號
            oPayment.Send.CustomField3 = "";
            oPayment.Send.CustomField4 = "";
            oPayment.Send.EncryptType = 1;

            //訂單的商品資料
            var buyitems = eCPayViewModel.ListItems;
            foreach (var buyitem in buyitems)
            {
                oPayment.Send.Items.Add(new ECPay.Payment.Integration.Item()
                {
                    Name = buyitem.Name,//商品名稱
                    Price = buyitem.Price,//商品單價
                    Currency = "新台幣",//固定幣別單位
                    Quantity = buyitem.Quantity,//購買數量
                    URL = "",//商品的說明網址
                });
            }


            var html = string.Empty;
            oPayment.CheckOutString(ref html);
            return html;
        }

        /// <summary>
        /// 送送會員付款成功
        /// </summary>
        /// <param name="id"></param>
        public void BecomVIPSuccess(int id)
        {
            //製作Rank欄位
            var rank = new Rank()
            {
                RankId = 30,
                MemberId = id,
                IsPrimary = true,
                EndTime = DateTime.UtcNow.AddHours(8).AddDays(30),
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

        /// <summary>
        /// 建立ECPayayRecord資料
        /// </summary>
        /// <param name="getResultFromECPay"></param>
        public void SaveECPayRecordTable(GetResultFromECPay getResultFromECPay)
        {
            ECPayRecord record = new ECPayRecord
            {
                //暫用固定ID之後要改
                ECPayRecordID = 1,
                Payment = getResultFromECPay.PaymentType,
                TradeNo = (int)long.Parse(getResultFromECPay.TradeNo),
                CheckMacValue = getResultFromECPay.CheckMacValue
            };
            if (getResultFromECPay.RtnCode == 1)
            {
                record.IsPaySuccess = true;
            }
            else
            {
                record.IsPaySuccess = false;
            }

            DbContext context = new HolyShongContext();
            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    _repo.Create<ECPayRecord>(record);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
        }

        /// <summary>
        /// 成功購買購物車商品後，清空購物車
        /// </summary>
        /// <param name="id"></param>
        public void BuyCartIsSuccess(int id)
        {
            var deletdeCart = _repo.GetAll<Cart>().First(x => x.MemberId == id);
            var deleteCartItems = deletdeCart.Item.ToList();
            List<ItemDetail> deleteCartItemsDetails = new List<ItemDetail>();
            //要問的
            foreach (var deldetCartItem in deleteCartItems)
            {
                var deleteCartItemDetails = _repo.GetAll<ItemDetail>().Where(x => x.ItemId == deldetCartItem.ItemId).ToList();

                //deleteCartItemsDetails.Add(deleteCartItemDetails);
            };

            DbContext context = new HolyShongContext();
            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    _repo.Delete<Cart>(deletdeCart);
                    foreach (var newCartItem in deleteCartItems)
                    {
                        _repo.Delete<Item>(newCartItem);
                    }

                    //_repo.Delete<ItemDetail>();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
        }
    }

}