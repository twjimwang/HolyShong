using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.HolyShongModels;

namespace HolyShong.Services
{
    public class CartService
    {
        public HolyShongRepository _holyShongRepository;
        public CartService()
        {
            _holyShongRepository = new HolyShongRepository();
        } 

        public List<CartViewModel> GetCartViewModels()
        {
            List<Cart> carts = _holyShongRepository.GetCarts();
            List<Item> items = _holyShongRepository.GetItems();
            List<ItemDetail> itemDetails = _holyShongRepository.GetItemDetail();
            List<ProductOption> productOptions = _holyShongRepository.GetProductOptions();
            List<ProductOptionDetail> productOptionDetails = _holyShongRepository.GetProductOptionDetails();
            List<Store> stores = _holyShongRepository.GetStores();
            List<Product> products = _holyShongRepository.GetProducts();
            List<Address> addresses = _holyShongRepository.GetAddresses();
            var CartTest =
                from c in carts
                join i in items
                on c.CartId equals i.CartId
                join itemD in itemDetails
                on i.ItemId equals itemD.ItemId
                join po in productOptionDetails
                on itemD.ProductOptionDetailId equals po.ProductOptionDetailId
                join p in productOptions
                on po.ProductOptionId equals p.ProductOptionId
                join s in stores
                on c.StroreId equals s.StoreId
                join pr in products
                on p.ProductId equals pr.ProductId
                join a in addresses
                on c.MemberId equals a.MemberId
                select new CartViewModel
                {   
                    StoreName = s.Name.ToString(),
                    ProductName = pr.Name.ToString(),
                    ProductOption = p.Name.ToString(),
                    ProductOptionDetail = po.Name.ToString(),
                    Price = pr.UnitPrice,
                    Address = a.Address1.ToString(),
                    IsTableWares = c.IsTablewares,
                    IsBag = c.IsBag
                };


            return (List<CartViewModel>) CartTest.ToList();
        }




    }
}