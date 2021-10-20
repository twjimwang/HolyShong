using System;
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


        /// <summary>
        /// 購物車到結帳頁面(暫無使用)
        /// </summary>
        //public CartViewModel AddToCart(List<StoreProduct> productCard, int memberId)
        //{
        //    CartViewModel cartVM = new CartViewModel();
        //    //初始化OperationResult
        //    OperationResult result = new OperationResult();

        //    //存進資料庫
        //    var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
        //    var firstProduct = productCard[0];
        //    var product = _repo.GetAll<Product>().FirstOrDefault(p => p.ProductId == firstProduct.ProductId);
        //    var productCate = _repo.GetAll<ProductCategory>().FirstOrDefault(pc => pc.ProductCategoryId == product.ProductCategoryId);
        //    var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == productCate.StoreId);

        //    Cart cart;

        //    DbContext context = new HolyShongContext();
        //    using (var tran = context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            cart = new Cart()
        //            {
        //                MemberId = member.MemberId,
        //                IsTablewares = false,
        //                IsPlasticbag = false,
        //                StroreId = store.StoreId,
        //            };
        //            //先創才能拿到CartId
        //            _repo.Create(cart);
        //            _repo.SaveChange();

        //            foreach (var pd in productCard)
        //            {
        //                Models.HolyShongModel.Item item = new Models.HolyShongModel.Item()
        //                {
        //                    CartId = cart.CartId,
        //                    ProductId = pd.ProductId,
        //                    Quantity = pd.Quantity,
        //                };
        //                _repo.Create(item);
        //                _repo.SaveChange();

        //                foreach (var productOption in pd.StoreProductOptions)
        //                {
        //                    ItemDetail itemDetail = new ItemDetail()
        //                    {
        //                        ItemId = item.ItemId,
        //                        ProductOptionDetailId = Int32.Parse(productOption.SelectOption)
        //                    };
        //                    _repo.Create(itemDetail);

        //                }
        //                _repo.SaveChange();
        //            }
        //            result.IsSuccessful = true;
        //            //改組成VM去CheckOut頁面
        //            cartVM = GetCheckOutByCart(cart);
        //            tran.Commit();
        //            return cartVM;
        //        }
        //        catch (Exception ex)
        //        {
        //            result.IsSuccessful = false;
        //            result.Exception = ex;
        //            tran.Rollback();
        //            return cartVM;
        //        }
        //    }
        //}

        //取得結帳頁面VM
        //public CartViewModel GetCheckOutByCart(Cart cart)
        //{
        //    var customerAddress = _repo.GetAll<Address>().FirstOrDefault(a=>a.MemberId == cart.MemberId && a.IsDefault == true);
        //    var store = _repo.GetAll<Store>().FirstOrDefault(s=>s.StoreId == cart.StroreId);
            
        //    var result = new CartViewModel();
        //    result.CreatedDate = DateTime.UtcNow;
        //    result.CustomerAddress = customerAddress.AddressDetail;
        //    result.IsPlasticbag = false;
        //    result.IsTablewares = false;
        //    result.StoreName = store.Name;
        //    result.StoreId = store.StoreId;
        //    result.StoreAddress = store.Address; 
        //    result.CartItems = new List<StoreProduct>();

        //    var items = _repo.GetAll<Models.HolyShongModel.Item>().Where(i=> i.CartId == cart.CartId);
        //    var itemDetails = _repo.GetAll<ItemDetail>().Where(id=> items.Select(i=> i.ItemId).Contains(id.ItemId));
        //    var products = _repo.GetAll<Product>().Where(p=> items.Select(i=>i.ProductId).Contains(p.ProductId));
        //    var productOptionDetails = _repo.GetAll<ProductOptionDetail>().Where(pod => itemDetails.Select(id => id.ProductOptionDetailId).Contains(pod.ProductOptionDetailId));
        //    var productOptions = _repo.GetAll<ProductOption>().Where(po => productOptionDetails.Select(pod => pod.ProductOptionId).Contains(po.ProductOptionId));

        //    foreach (var item in items)
        //    {
        //        var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
        //        var tempItem = new CartItem()
        //        {
        //            ProductId = item.ProductId,
        //            ProductName = product.Name,
        //            UnitPrice = product.UnitPrice,
        //            Quantity = item.Quantity,
        //            StoreProductOptions = new List<StoreProductOption>()
        //        };

        //        foreach (var option in productOptions.Where(po=>po.ProductId == item.ProductId))
        //        {
        //            var tempOption = new StoreProductOption()
        //            {
        //                ProductOptionName = option.Name,
        //                ProductOptionDetails = new List<StoreProductOptionDetail>()
        //            };

        //            foreach(var detail in productOptionDetails.Where(pod=> option.ProductOptionId == pod.ProductOptionId))
        //            {
        //                var tempDetail = new StoreProductOptionDetail()
        //                {
        //                    StoreProductOptionDetailId = detail.ProductOptionDetailId,
        //                    StoreProductOptioinDetailName = detail.Name,
        //                    AddPrice = detail.AddPrice == null ? 0 : (decimal)detail.AddPrice
        //                };
        //                tempOption.ProductOptionDetails.Add(tempDetail);
        //            }
        //            tempItem.StoreProductOptions.Add(tempOption);
        //        }
        //            result.CartItems.Add(tempItem);
        //    }
        //    return result;
        //}

        /// <summary>
        /// 結帳頁面
        /// </summary>
        public CartViewModel ToCheckOut(List<StoreProduct> productCard, int memberId)
        {
            CartViewModel cartVM = new CartViewModel();

            var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
            var customerAddress = _repo.GetAll<Address>().FirstOrDefault(a => a.MemberId == member.MemberId && a.IsDefault == true);

            var firstProduct = productCard[0];
            var product = _repo.GetAll<Product>().FirstOrDefault(p => p.ProductId == firstProduct.ProductId);
            var productCate = _repo.GetAll<ProductCategory>().FirstOrDefault(pc => pc.ProductCategoryId == product.ProductCategoryId);
            var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == productCate.StoreId);

            cartVM.StoreId = store.StoreId;
            cartVM.StoreName = store.Name;
            cartVM.CustomerAddress = customerAddress.AddressDetail;
            cartVM.Note = String.Empty;
            cartVM.IsPlasticbag = false;
            cartVM.IsTablewares = false;
            cartVM.CreatedDate = DateTime.UtcNow;
            cartVM.CartItems = productCard;


            return cartVM;
        }

        /// <summary>
        /// 成立訂單
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="session"></param>
        /// <param name="checkoutVM"></param>
        /// <returns></returns>
        public OperationResult OrderCreate(int memberId,List<StoreProduct> session,CheckOutViewModel checkoutVM)
        {
            //初始化OperationResult
            OperationResult result = new OperationResult();
            //抓Store
            var product = _repo.GetAll<Product>().FirstOrDefault(p => p.ProductId == (int)session[0].ProductId);
            var productCate = _repo.GetAll<ProductCategory>().FirstOrDefault(pc => pc.ProductCategoryId == product.ProductCategoryId);
            var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == productCate.StoreId);

            DbContext context = new HolyShongContext();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    //刪除其他未付款的訂單
                    var notPaidOrders = _repo.GetAll<Order>().Where(o => o.PaymentStatus == 0 && o.OrderStatus == 0);
                    var notPaidOrderDetails = _repo.GetAll<OrderDetail>().Where(od=> notPaidOrders.Select(npo => npo.OrderId).Contains(od.OrderId));
                    var notPaidOrderDetailOptions = _repo.GetAll<OrderDetailOption>().Where(npodo => notPaidOrderDetails.Select(npod=> npod.OrderDetailId).Contains(npodo.OrderDetailId));

                    if (notPaidOrderDetailOptions != null)
                    {
                        foreach (var npodo in notPaidOrderDetailOptions)
                        {
                            _repo.Delete(npodo);
                        }
                    }

                    if(notPaidOrderDetails != null)
                    {
                        foreach (var npod in notPaidOrderDetails)
                        {
                            _repo.Delete(npod);
                        }
                    }

                    if(notPaidOrders != null)
                    {
                        foreach (var npo in notPaidOrders)
                        {
                            _repo.Delete(npo);
                        }
                    }
                    _repo.SaveChange();



                    //加入新order
                    Order order = new Order()
                    {
                        MemberId = memberId,
                        StoreId = store.StoreId,
                        DeliveryFee = 30, //待確認
                        Notes = checkoutVM.CustomerNote, 
                        DeliveryAddress = checkoutVM.CustomerAddress,
                        IsTablewares = checkoutVM.IsTablewares,
                        IsPlasticbag = checkoutVM.IsPlasticbag,
                        PaymentStatus = 0,
                        DeliverStatus = 0,
                        OrderStatus = 0,
                        CreateDate = DateTime.UtcNow,
                        RequiredDate = DateTime.UtcNow,
                        OrderStatusUpdateTime = DateTime.UtcNow
                    };
                    //先存才能拿到orderId
                    _repo.Create(order);
                    _repo.SaveChange();


                    foreach (var p in session)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            UnitPrice = product.UnitPrice,
                            Quantity = p.Quantity
                        };
                        _repo.Create(orderDetail);
                        _repo.SaveChange();
                        //先存才能拿到orderId

                        if (p.StoreProductOptions != null)
                        {
                            foreach (var option in p.StoreProductOptions)
                            {

                                OrderDetailOption orderDetailOption = new OrderDetailOption()
                                {
                                    OrderDetailId = orderDetail.OrderDetailId,
                                    ProductOptionDetailId = _repo.GetAll<ProductOptionDetail>().FirstOrDefault(pod => pod.Name == option.ProductOptionName).ProductOptionDetailId,
                                };
                                _repo.Create(orderDetail);
                                _repo.SaveChange();
                            }
                        }
                    }

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
            //刪除購物車cookie?
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
            var deliver = _repo.GetAll<Deliver>().FirstOrDefault(d => d.DeliverId == order.DeliverId);
            if (deliver != null)
            {
                var member = _repo.GetAll<Member>().First(m => m.MemberId == deliver.MemberId);
                orderResult.DeliverName = member.LastName + member.FirstName;
                orderResult.DeliverImg = deliver.HeadshotImg;
            }
           
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



        //外送員


        /// <summary>
        /// 外送員上下線切換
        /// </summary>
        /// <param name="connectionStatus"></param>
        /// <returns></returns>
        public bool SwitchDeliverConnection(DeliverConnectionViewModel deliverConnectionVM)
        {
            //透過會員狀態關連到deliverId
            var deliverId = _repo.GetAll<Deliver>().FirstOrDefault(d=>d.MemberId == deliverConnectionVM.memberId).DeliverId;

            //找出外送員有沒有在外送，有的話駁回切換下線
            var deliverStatus = _repo.GetAll<Deliver>().FirstOrDefault(d => d.DeliverId == deliverId).isDelivering;
            if (deliverStatus == true)
            {
                deliverConnectionVM.isOnline = !deliverConnectionVM.isOnline;
                return deliverConnectionVM.isOnline;
            }

            //其餘儲存狀態修改
            //VM->DM
            var deliverInfo = _repo.GetAll<Deliver>().FirstOrDefault(d => d.DeliverId == deliverId);

            deliverInfo.isOnline = deliverConnectionVM.isOnline;

            //update+savechange
            _repo.Update(deliverInfo);
            _repo.SaveChange();

            return deliverConnectionVM.isOnline;
        }

        /// <summary>
        /// 抓外送員判斷資料庫中上下線
        /// </summary>
        /// <returns></returns>
        public Deliver GetDeliverInfo(string memberEmail)
        {
            var memberId = _repo.GetAll<Member>().FirstOrDefault(m=>m.Email == memberEmail).MemberId;
            var deliver = _repo.GetAll<Deliver>().FirstOrDefault(d => d.MemberId == memberId);

            return deliver;
        }

        /// <summary>
        /// 外送員外送畫面
        /// </summary>
        /// <returns></returns>
        public DeliverViewModel GetOrderForDeliver(int memberId)
        {
            DeliverViewModel result = new DeliverViewModel() { OrderProducts = new List<OrderProducts>() };

            //透過帳號登入抓到這個人的memberId & DeliverId
            var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
            var deliver = _repo.GetAll<Deliver>().FirstOrDefault(d => d.MemberId == memberId);


            //透過deliverId 去抓出他的外送訂單中，運送中的單(orderstatus = 4 && deliverStatus = 1)，取orderId
            var order = _repo.GetAll<Order>().Where(o => o.DeliverId ==deliver.DeliverId).FirstOrDefault(o => o.OrderStatus == 4 || o.OrderStatus == 5);
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
            //抓memberId
            var memberId = OrderStatusVM.MemberId;

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
                    if (OrderStatusVM.OrderStatus == 4)
                    {
                        //var notSelf = _repo.GetAll<Deliver>().Where(d => d.MemberId != memberId);
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

    }
}


