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

                select new CartViewModel
                {
                    ProductName = c.CartId.ToString(),
                    ProductOption = po.ProductOptionId.ToString(),
                    ProductOptionDetail = itemD.ProductOptionDetailId.ToString(),
                    IsTableWares = c.IsTablewares,
                    IsBag = c.IsBag

                };


            return (List<CartViewModel>)CartTest.ToList();
        }




    }
}