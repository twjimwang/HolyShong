using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Models;

namespace HolyShong.Repositories
{
    public class OrderRepository
    {
        public HolyShongContext _ctx;
        public OrderRepository()
        {
            _ctx = new HolyShongContext();
        }

        public List<Order> GetOrder()
        {
            return _ctx.Order.ToList();
        }
        public List<Member> GetMembers()
        {
            return _ctx.Member.ToList();
        }
        public List<OrderDetail> GetOrderDetails()
        {
            return _ctx.OrderDetail.ToList();
        }
        public List<Deliver> GetDelivers()
        {
            return _ctx.Deliver.ToList();
        }
        public List<Address> GetAddress()
        {
            return _ctx.Address.ToList();
        }
        public List<Store> GetStore()
        {
            return _ctx.Store.ToList();
        }
        public List<Product> GetProduct()
        {
            return _ctx.Product.ToList();
        }
        public List<ProductCategory> GetProductCategory()
        {
            return _ctx.ProductCategory.ToList();
        }
    }
}