using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models;

namespace HolyShong.Services
{
    public class OrderService
    {
        //初始化repository
        public OrderRepository _repo;
        public OrderService()
        {
            _repo = new OrderRepository();
        }

        //DM -> VM
        public List<OrderDeliverVM> GetOrderVM()
        {
            #region 封印
            //一定要呼叫全部表單再join嗎? 如何做關聯
            //List<Order> orders = _repo.GetOrder();
            //List<Deliver> delivers = _repo.GetDelivers();
            //List<Member> members = _repo.GetMembers();
            //List<Store> stores = _repo.GetStore();
            //List<OrderDetail> orderDetails = _repo.GetOrderDetails();
            //List<Product> products = _repo.GetProduct();
            //List<ProductCategory> productCategories = _repo.GetProductCategory();


            ////DM -> VM
            //var productVM = from o in orders
            //           join od in orderDetails
            //           on o.OrderId equals od.OrderId
            //           join s in stores
            //           on o.StoreId equals s.StoreId
            //           join pc in productCategories
            //           on s.StoreId equals pc.StoreId
            //           join p in products
            //           on pc.ProductCategoryId equals p.ProductCategoryId
            //           where o.OrderId == 1
            //           select new OrderProductVM
            //           {
            //                ProductName = p.Name,
            //                ProductQuantity = od.Quantity,
            //                ProductUnitPrice = od.UnitPrice
            //           };
            //var productVMList = productVM.ToList();

            ////DM -> VM
            //var odVM = from o in orders
            //           join d in delivers
            //           on o.DeliverId equals d.DeliverId
            //           join m in members
            //           on d.MemberId equals m.MemberId
            //           join s in stores
            //           on o.StoreId equals s.StoreId
            //           join od in orderDetails
            //           on o.OrderId equals od.OrderId
            //           join pc in productCategories
            //           on s.StoreId equals pc.StoreId
            //           join p in products
            //           on pc.ProductCategoryId equals p.ProductCategoryId
            //           where o.OrderId == 1
            //           where p.ProductId == 1
            //           select new OrderDeliverVM
            //           {
            //               DeliverName = m.FirstName + m.LastName,
            //               Address = o.DeliveryAddress,
            //               Notes = o.Notes,
            //               OrderProduct = productVMList,
            //               StoreName = s.Name,
            //           };

            //產品動態產生儲存方式            
            #endregion

            var result = new List<OrderDeliverVM>();
            var orders = _repo.GetOrder().ToList();
            var delivers = _repo.GetDelivers().Where(x => orders.Select(o => o.DeliverId).Contains(x.DeliverId)).Distinct().ToList();
            var members = _repo.GetMembers().Where(x => delivers.Select(d => d.MemberId).Contains(x.MemberId)).ToList();
            var stores = _repo.GetStore().Where(x => orders.Select(o => o.StoreId).Distinct().Contains(x.StoreId)).ToList();
            var orderdetails = _repo.GetOrderDetails().Where(od => orders.Select(o => o.OrderId).Contains(od.OrderId)).ToList();
            var orderProducts = _repo.GetProduct().Where(p => orderdetails.Select(od => od.ProductId).Contains(p.ProductId)).ToList();

            foreach(var order in orders)
            {
                var deliver = delivers.First(d => d.DeliverId == order.DeliverId);
                var member = members.First(m => m.MemberId == deliver.MemberId);
                var store = stores.First(s => s.StoreId == order.StoreId);
                var ods = orderdetails.Where(x => x.OrderId == order.OrderId).ToList();
                var ops = orderProducts.Where(op => ods.Select(x => x.ProductId).Contains(op.ProductId)).ToList();
                var products = ods.Select(x => new OrderProductVM
                {
                    ProductName = ops.First(p => p.ProductId == x.ProductId).Name,
                    ProductQuantity = x.Quantity,
                    ProductUnitPrice = x.UnitPrice
                }).ToList();

                decimal total = 0;
                foreach(var p in products)
                {
                    total += p.ProductQuantity * p.ProductUnitPrice;
                }

                var temp = new OrderDeliverVM
                {
                    DeliverName = $"{member.LastName},{member.FirstName}",
                    Address = order.DeliveryAddress,
                    Notes = order.Notes,
                    StoreName = store.Name,
                    OrderProduct = products,
                    TotalPrice = total
                };
                result.Add(temp);
            }

            return result;
            //return odVM.ToList();


        }
    }
}