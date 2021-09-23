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

        public IQueryable<Order> GetOrder()
        {
            return _ctx.Order;
        }
        public IQueryable<Member> GetMembers()
        {
            return _ctx.Member;
        }
        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return _ctx.OrderDetail;
        }
        public IQueryable<Deliver> GetDelivers()
        {
            return _ctx.Deliver;
        }
        public IQueryable<Address> GetAddress()
        {
            return _ctx.Address;
        }
        public IQueryable<Store> GetStore()
        {
            return _ctx.Store;
        }
        public IQueryable<Product> GetProduct()
        {
            return _ctx.Product;
        }
        public IQueryable<ProductCategory> GetProductCategory()
        {
            return _ctx.ProductCategory;
        }
    }
}