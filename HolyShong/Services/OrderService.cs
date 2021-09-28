using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;

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
        /// 單筆外送訂單呈現
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
            if(order == null)
            {
                return orderResult;
            }

            var deliver = _repo.GetAll<Deliver>().First(d => d.DeliverId == order.DeliverId);
            var member = _repo.GetAll<Member>().First(m => m.MemberId == deliver.MemberId);
            var store = _repo.GetAll<Store>().First(s => s.StoreId == order.StoreId);
            var orderDetail = _repo.GetAll<OrderDetail>().Where(od => od.OrderId == order.OrderId);
            var product = _repo.GetAll<Product>().Where(p => orderDetail.Select(od=>od.ProductId).Contains(p.ProductId));

            var productlist = orderDetail.Select(od => new OrderList
            {
                ProductName = product.FirstOrDefault(p=>p.ProductId == od.ProductId).Name,
                ProductPrice = od.UnitPrice,
                ProductQuantity = od.Quantity
            }).ToList();

            foreach(var p in productlist)
            {
                total += p.ProductPrice * p.ProductQuantity;
            }

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
            if(member == null)
            {
                return new List<OrderListViewModel>();
            }

            var orders = _repo.GetAll<Order>().Where(o => o.MemberId == member.MemberId);
            var stores = _repo.GetAll<Store>().Where(s => orders.Select(o => o.StoreId).Contains(s.StoreId));
            var orderDetails = _repo.GetAll<OrderDetail>().Where(od => orders.Select(o => o.OrderId).Contains(od.OrderId));
            var orderOptionDetails = _repo.GetAll<OrderDetailOption>().Where(odo => orderDetails.Select(od=>od.OrderDetailId).Contains(odo.OrderDetailId));
            var products = _repo.GetAll<Product>().Where(p => orderDetails.Select(od => od.ProductId).Contains(p.ProductId));
            var productOptions = _repo.GetAll<ProductOption>().Where(po => orderOptionDetails.Select(odo => odo.ProductOptionId).Contains(po.ProductOptionId));
            var productOptionDetails = _repo.GetAll<ProductOptionDetail>().Where(pod => orderOptionDetails.Select(odo=>odo.ProductOptionDetailId).Contains(pod.ProductOptionDetailId));


            foreach (var o in orders)
            {
                var productNumber = 0;
                //ProductName 需要orderDetail Product productOptionDetail
                var orderDetail = orderDetails.Where(od => od.OrderId == o.OrderId);
                var orderOptionDetail = orderOptionDetails.Where(ood => orderDetail.Select(od => od.OrderDetailId).Contains(ood.OrderDetailId));
                var productOptionDetail = productOptionDetails.Where(pod => orderOptionDetail.Select(ood => ood.ProductOptionDetailId).Contains(pod.ProductOptionDetailId)).
                    Select(pod => pod.Name);
                var productOptionDetailString = String.Join("．", productOptionDetail);
                var product = products.Where(p => orderDetail.Select(od => od.ProductId).Contains(p.ProductId)).
                    Select(p => new OrderProduct
                    {
                        ProductName = p.Name,
                        ProductPrice = orderDetail.FirstOrDefault(od => od.ProductId == p.ProductId).UnitPrice,
                        ProductQuantity = orderDetail.FirstOrDefault(od => od.ProductId == p.ProductId).Quantity,
                        OrderOptions = productOptionDetailString,
                    }).ToList();
                foreach(var p in product)
                {
                    productNumber += p.ProductQuantity;
                }

                var amount = orderDetail.Sum(od => od.Quantity * od.UnitPrice);
                decimal addPrice = orderOptionDetail.Sum(ood => ood.AddPrice).HasValue? orderOptionDetail.Sum(ood => ood.AddPrice).Value : 0;
                var tempOrder = new OrderListViewModel
                {
                    OrderId = o.OrderId,
                    DeliverDate = o.RequiredDate == null ? o.CreateDate : o.RequiredDate,
                    RestaurantId = stores.FirstOrDefault(s => s.StoreId == o.StoreId).StoreId,
                    RestaurantName = stores.FirstOrDefault(s => s.StoreId == o.StoreId).Name,
                    RestaurantImg = stores.FirstOrDefault(s => s.StoreId == o.StoreId).Img,
                    ProductLists = product,
                    ProductCount = productNumber,
                    OrderStatus = o.OrderStatus ? 1 : 0,
                    Total = amount + addPrice
                };
                result.Add(tempOrder);
            }
            return result;
        }

        /// <summary>
        /// 外送員外送訂單呈現
        /// </summary>
        /// <returns></returns>
        public DeliverViewModel GetOrderForDeliver(int orderId)
        {
            DeliverViewModel result = new DeliverViewModel() { OrderProducts = new List<OrderProducts>() };
            var order = _repo.GetAll<Order>().FirstOrDefault(o => o.OrderId == orderId);
            if(order == null)
            {
                return new DeliverViewModel();
            }
            var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == order.MemberId);
            var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == order.OrderId);
            var orderDetails = _repo.GetAll<OrderDetail>().Where(od => od.OrderId == order.OrderId);
            var products = _repo.GetAll<Product>().Where(p => orderDetails.Select(od=>od.ProductId).Contains(p.ProductId));

            result.OrderCode = "EAT" + order.OrderId.ToString().PadLeft(5,'0');
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

            //var orders
            return result;
        }
    }
}








////取得表單
//var orders = _repo.GetAll<Order>().Where(o => o.OrderId == orderId);
//var delivers = _repo.GetAll<Deliver>().Where(d => orders.Select(o => o.DeliverId).Contains(d.DeliverId)).Distinct();
//var members = _repo.GetAll<Member>().Where(m => delivers.Select(d => d.MemberId).Contains(m.MemberId));
//var stores = _repo.GetAll<Store>().Where(s => orders.Select(o => o.StoreId).Contains(s.StoreId));
//var orderDetails = _repo.GetAll<OrderDetail>().Where(od => orders.Select(o => o.OrderId).Contains(od.OrderId));
//var orderProducts = _repo.GetAll<Product>().Where(p => orderDetails.Select(od => od.ProductId).Contains(p.ProductId));

////傳入VM
//orderResult = orders.Select(o => new OrderDeliverViewModel
//{
//    DeliverName = members.First(m => m.MemberId == d)
//});