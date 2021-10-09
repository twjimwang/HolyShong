﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;
using System.Data.Entity;

namespace HolyShong.Services
{
    public class OrderService
    {
        private readonly HolyShongRepository _repo;
        public OrderService()
        {
            _repo = new HolyShongRepository();
        }

        public OperationResult Create(HolyCartViewModel cartVM)
        {
            //初始化OperationResult
            OperationResult result = new OperationResult();

            //抓到memberID
            var memberId = 1;

            DbContext context = new HolyShongContext();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    //加入order
                    Order order = new Order()
                    {
                        MemberId = memberId,
                        StoreId = _repo.GetAll<Store>().FirstOrDefault(s => s.Name == cartVM.StoreName).StoreId,
                        DeliveryFee = 30, //待確認
                        Notes = "等資料庫新建cart欄位", //待確認
                        DeliveryAddress = cartVM.Address,
                        IsTablewares = false, //待確認
                        IsPlasticbag = false, //待確認
                        PaymentStatus = 1,
                        DeliverStatus = 0,
                        OrderStatus = 1,
                        CreateDate = DateTime.UtcNow,
                        RequiredDate = DateTime.UtcNow
                        //OrderSatutsUpdateTime待確認
                    };
                    _repo.Create(order);
                    //先存才能拿到orderId
                    //_repo.SaveChange();


                    //加入orderDetail
                    var products = _repo.GetAll<Product>().Where(p => cartVM.CartItems.Select(c => c.ProductName).Contains(p.Name));
                    foreach (var item in cartVM.CartItems)
                    {
                        var product = products.FirstOrDefault(p => p.Name == item.ProductName);
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            UnitPrice = product.UnitPrice,
                            Quantity = 2,  //待確認
                            //Description
                        };
                    }

                    //加入orderDetailOption


                    result.IsSuccessful = true;
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    result.IsSuccessful = false;
                    result.Exception = ex;
                    tran.Rollback();
                }
            }
            return result;
            //刪除購物車?
        }

        /// <summary>
        /// 歷史訂單透過orderId連接
        /// </summary>
        /// <returns></returns>
        public OrderDeliverViewModel GetOrder(int orderId)
        {
            decimal total = 0;  //計算訂單總金額
            OrderDeliverViewModel orderResult = new OrderDeliverViewModel()
            {
                OrderLists = new List<OrderList>()
            };

            var order = _repo.GetAll<Order>().FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return orderResult;
            }

            //若還沒有外送員
            var deliver = _repo.GetAll<Deliver>().First(d => d.DeliverId == order.DeliverId);
            var member = _repo.GetAll<Member>().First(m => m.MemberId == deliver.MemberId);
            var store = _repo.GetAll<Store>().First(s => s.StoreId == order.StoreId);
            var orderDetail = _repo.GetAll<OrderDetail>().Where(od => od.OrderId == order.OrderId);
            var product = _repo.GetAll<Product>().Where(p => orderDetail.Select(od => od.ProductId).Contains(p.ProductId));

            var productlist = orderDetail.Select(od => new OrderList
            {
                ProductName = product.FirstOrDefault(p => p.ProductId == od.ProductId).Name,
                ProductPrice = od.UnitPrice,
                ProductQuantity = od.Quantity
            }).ToList();

            foreach (var p in productlist)
            {
                total += p.ProductPrice * p.ProductQuantity;
            }

            orderResult.OrderStatus = order.OrderStatus;
            orderResult.DeliverName = member.LastName + member.FirstName;
            orderResult.DeliverImg = deliver.HeadshotImg;
            orderResult.CustomerAddress = order.DeliveryAddress;
            orderResult.CustomerNotes = order.Notes;
            orderResult.RestaurantName = store.Name;
            orderResult.RestaurantAddress = store.Address;
            orderResult.OrderLists = productlist;
            orderResult.Total = total;

            return orderResult;
        }

        /// <summary>
        /// 查詢會員歷史訂單
        /// </summary>
        public List<OrderListViewModel> GetOrderByMemberId(int memberId)
        {
            List<OrderListViewModel> result = new List<OrderListViewModel>();

            var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
            if (member == null)
            {
                return new List<OrderListViewModel>();
            }

            var orders = _repo.GetAll<Order>().Where(o => o.MemberId == member.MemberId);
            var stores = _repo.GetAll<Store>().Where(s => orders.Select(o => o.StoreId).Contains(s.StoreId));
            var orderDetails = _repo.GetAll<OrderDetail>().Where(od => orders.Select(o => o.OrderId).Contains(od.OrderId));
            var orderOptionDetails = _repo.GetAll<OrderDetailOption>().Where(odo => orderDetails.Select(od => od.OrderDetailId).Contains(odo.OrderDetailId));
            var products = _repo.GetAll<Product>().Where(p => orderDetails.Select(od => od.ProductId).Contains(p.ProductId));
            var productOptionDetails = _repo.GetAll<ProductOptionDetail>().Where(pod => orderOptionDetails.Select(odo => odo.ProductOptionDetailId).Contains(pod.ProductOptionDetailId));
            var productOptions = _repo.GetAll<ProductOption>().Where(po => productOptionDetails.Select(pod => pod.ProductOptionId).Contains(po.ProductOptionId));

            foreach (var o in orders)
            {
                var productNumber = 0;
                //ProductName 需要orderDetail Product productOptionDetail
                var orderDetail = orderDetails.Where(od => od.OrderId == o.OrderId);
                var orderOptionDetail = orderOptionDetails.Where(ood => orderDetail.Select(od => od.OrderDetailId).Contains(ood.OrderDetailId));
                var productOptionDetail = productOptionDetails.Where(pod => orderOptionDetail.Select(ood => ood.ProductOptionDetailId).Contains(pod.ProductOptionDetailId));
                var productOptionDetailName = productOptionDetail.Select(pod => pod.Name);
                var productOptionDetailString = String.Join("．", productOptionDetailName);
                var product = products.Where(p => orderDetail.Select(od => od.ProductId).Contains(p.ProductId)).
                    Select(p => new OrderProduct
                    {
                        ProductName = p.Name,
                        ProductPrice = orderDetail.FirstOrDefault(od => od.ProductId == p.ProductId).UnitPrice,
                        ProductQuantity = orderDetail.FirstOrDefault(od => od.ProductId == p.ProductId).Quantity,
                        OrderOptions = productOptionDetailString,
                    }).ToList();
                foreach (var p in product)
                {
                    productNumber += p.ProductQuantity;
                }

                var amount = orderDetail.Sum(od => od.Quantity * od.UnitPrice);
                decimal addPrice = productOptionDetail.Sum(pod => pod.AddPrice).HasValue ? productOptionDetail.Sum(pod => pod.AddPrice).Value : 0;
                var tempOrder = new OrderListViewModel
                {
                    OrderId = o.OrderId,
                    DeliverDate = o.RequiredDate == null ? o.CreateDate : o.RequiredDate,
                    RestaurantId = stores.FirstOrDefault(s => s.StoreId == o.StoreId).StoreId,
                    RestaurantName = stores.FirstOrDefault(s => s.StoreId == o.StoreId).Name,
                    RestaurantImg = stores.FirstOrDefault(s => s.StoreId == o.StoreId).Img,
                    ProductLists = product,
                    ProductCount = productNumber,
                    OrderStatus = o.OrderStatus,
                    Total = amount + addPrice
                };
                result.Add(tempOrder);
            }
            return result;
        }

        /// <summary>
        /// 外送員上下線切換
        /// </summary>
        /// <param name="connectionStatus"></param>
        /// <returns></returns>
        public bool SwitchDeliverConnection(bool connectionStatus)
        {
            //透過會員狀態關連到deliverId
            var deliverId = 1;

            //找出外送員有沒有在外送，有的話駁回切換下線
            var deliverStatus = _repo.GetAll<Deliver>().FirstOrDefault(d => d.DeliverId == deliverId).isDelivering;
            if (deliverStatus == true)
            {
                connectionStatus = !connectionStatus;
                return connectionStatus;
            }

            //其餘儲存狀態修改
            //VM->DM
            var deliverInfo = _repo.GetAll<Deliver>().FirstOrDefault(d => d.DeliverId == deliverId);

            deliverInfo.isOnline = connectionStatus;

            //update+savechange
            _repo.Update(deliverInfo);
            _repo.SaveChange();

            return connectionStatus;
        }

        /// <summary>
        /// 抓外送員判斷資料庫中上下線
        /// </summary>
        /// <returns></returns>
        public Deliver GetDeliverInfo()
        {
            var id = 1;

            var deliver = _repo.GetAll<Deliver>().FirstOrDefault(x => x.DeliverId == id);

            return deliver;
        }

        /// <summary>
        /// 外送員外送畫面
        /// </summary>
        /// <returns></returns>
        public DeliverViewModel GetOrderForDeliver()
        {
            DeliverViewModel result = new DeliverViewModel() { OrderProducts = new List<OrderProducts>() };

            //透過帳號登入抓到這個人的memberId & DeliverId
            var memberId = 1;
            var deliverId = 1;
            var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
            var deliver = _repo.GetAll<Deliver>().FirstOrDefault(d => d.MemberId == memberId);


            //透過deliverId 去抓出他的外送訂單中，運送中的單(orderstatus = 4 && deliverStatus = 1)，取orderId
            var order = _repo.GetAll<Order>().Where(o => o.DeliverId == deliverId).FirstOrDefault(o => o.OrderStatus == 4 || o.OrderStatus == 5);
            if (order == null)
            {
                return result;
            }

            //透過orderId取得訂單其他資訊
            var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == order.StoreId);
            var orderDetails = _repo.GetAll<OrderDetail>().Where(od => od.OrderId == order.OrderId);
            var products = _repo.GetAll<Product>().Where(p => orderDetails.Select(od => od.ProductId).Contains(p.ProductId));

            result.OrderCode = "EAT" + order.OrderId.ToString().PadLeft(5, '0');
            result.OrderStatus = order.OrderStatus;
            result.CustomerName = member.LastName + member.FirstName;
            result.CustomerAddress = order.DeliveryAddress;
            result.CustormerNotes = order.Notes;
            result.RestaurantName = store.Name;
            result.RestaurantAddress = store.Address;
            result.OrderProducts = orderDetails.Select(od => new OrderProducts
            {
                ProductName = products.FirstOrDefault(p => p.ProductId == od.ProductId).Name,
                ProductQuantity = od.Quantity

            }).ToList();
            return result;
        }

        /// <summary>
        /// 外送員訂單狀態與物流狀態改變
        /// </summary>
        public OperationResult ChangeOrderState(OrderStatusViewModel OrderStatusVM)
        {
            OperationResult result = new OperationResult();
            DbContext context = new HolyShongContext();
            //VM中分析他的orderID
            var orderId = Int32.Parse(string.Join("",OrderStatusVM.OrderCode.Skip(3).Take(5).Select(x => x)));
            //先抓到訂單
            var order = _repo.GetAll<Order>().FirstOrDefault(o => o.OrderId == orderId);

            //交易
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    //傳入訂單狀態判斷
                    //餐廳完成訂單，安排外送員
                    if (OrderStatusVM.OrderStatus == 3)
                    {
                        var freeDeliver = _repo.GetAll<Deliver>().Where(d => d.isOnline == true && d.isDelivering == false).OrderBy(d => d.DeliverId).First();
                        order.DeliverId = freeDeliver.DeliverId;
                        //外送員改成送貨中
                        freeDeliver.isDelivering = true;
                        order.OrderStatus = 4;

                        //update
                        _repo.Update(freeDeliver);
                        //_repo.SaveChange();
                    }
                    //開始外送
                    else if(OrderStatusVM.OrderStatus == 5)
                    {
                        //orderstate改變
                        order.OrderStatus = 5;
                        //deliverstatus改變
                        order.DeliverStatus = 2;
                    }
                    //完成外送
                    else if(OrderStatusVM.OrderStatus == 6)
                    {
                        order.OrderStatus = 6;
                        order.DeliverStatus = 3;

                        var deliver = _repo.GetAll<Deliver>().FirstOrDefault(d=>d.DeliverId == order.DeliverId);
                        deliver.isDelivering = false;
                        //外送員改成非外送中
                        _repo.Update(deliver);
                        _repo.SaveChange();
                    }
                    result.IsSuccessful = true;
                    tran.Commit();
                }
                catch(Exception ex)
                {
                    result.IsSuccessful = false;
                    result.Exception = ex;
                    tran.Rollback();
                }
            }
            //update
            _repo.Update(order);
            _repo.SaveChange();
            return result;

        }


        /// <summary>
        /// 從購物車取得Order
        /// </summary>
        public void PutShoppingCartToOrder()
        {

        }
    }
}


