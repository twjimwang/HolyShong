using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.HolyShongModels;

namespace HolyShong.Repositories
{
    public class HolyShongRepository
    {
        public HolyShongContext _ctx;
        public HolyShongRepository()
        {
            _ctx = new HolyShongContext();
        }

        public List<Cart> GetCarts()
        {
            return _ctx.Cart.ToList();
        }  

        public List<Item> GetItems()
        {
            return _ctx.Item.ToList();
        }

        public List<ItemDetail> GetItemDetail()
        {
            return _ctx.ItemDetail.ToList();
        }

        public List<ProductOption> GetProductOptions()
        {
            return _ctx.ProductOption.ToList();
        }

        public List<ProductOptionDetail> GetProductOptionDetails()
        {
            return _ctx.ProductOptionDetail.ToList();
        }

        public List<Store> GetStores()
        {
           return _ctx.Store.ToList();
        }

        public List<Product> GetProducts()
        {
            return _ctx.Product.ToList();
        }
        public List<Address> GetAddresses()
        {
            return _ctx.Address.ToList();
        }



    }
}