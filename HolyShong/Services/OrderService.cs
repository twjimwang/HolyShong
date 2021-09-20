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
            //一定要呼叫全部表單再join嗎? 如何做關聯
            List<Order> orders = _repo.GetOrder();
            List<Deliver> delivers = _repo.GetDelivers();
            List<Member> members = _repo.GetMembers();
            List<Store> stores = _repo.GetStore();
            List<OrderDetail> orderDetails = _repo.GetOrderDetails();
            List<Product> products = _repo.GetProduct();
            List<ProductCategory> productCategories = _repo.GetProductCategory();

            var odVM = from o in orders
                       join d in delivers
                       on o.DeliverId equals d.DeliverId
                       join m in members
                       on d.MemberId equals m.MemberId
                       join s in stores
                       on o.StoreId equals s.StoreId
                       join od in orderDetails
                       on o.OrderId equals od.OrderId
                       join pc in productCategories
                       on s.StoreId equals pc.StoreId
                       join p in products
                       on pc.ProductCategoryId equals p.ProductCategoryId
                       where o.OrderId == 1
                       where p.ProductId == 1
                       select new OrderDeliverVM
                       {
                           DeliverName = m.FirstName + m.LastName,
                           Address = o.DeliveryAddress,
                           Notes = o.Notes,
                           StoreName = s.Name,
                           ProductName = p.Name,
                           ProductQuantity = od.Quantity,
                           ProductUnitPrice = od.UnitPrice,
                           TotalPrice = od.Quantity * od.UnitPrice
                       };
            //產品動態產生儲存方式

            return odVM.ToList();


        }
    }
}