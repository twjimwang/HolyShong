using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Repositories;
using HolyShong.Models.HolyShongModel;
using System.Net;
using System.Web.Services;
using System.Web.Mvc;

namespace HolyShong.Services
{
    public class CartService
    {
        private readonly HolyShongRepository _repo;
        public CartService()
        {
            _repo = new HolyShongRepository();
        }

        public HolyCartViewModel GetCartByMemberId(int memberId)
        {
            var result = new HolyCartViewModel()
            {
                CartItems = new List<CartItem>()
               
            };
           

            // 用memberId 找出特定的member
            var member =_repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
            if( member == null)
            {
                return result;
            }

            // 用memberId找到 特定的cart
            var cart = _repo.GetAll<Cart>().FirstOrDefault(c => c.MemberId == memberId);
            var store = _repo.GetAll<Store>().FirstOrDefault(s => s.StoreId == cart.StroreId);
           
            // 找item裡面找到包含上面篩選出來的cart的cartId
            var items = _repo.GetAll<Item>().Where(i => i.CartId == cart.CartId);
            var itemDetails = _repo.GetAll<ItemDetail>().Where(id => items.Select(i => i.ItemId).Contains(id.ItemId));
            var products = _repo.GetAll<Product>().Where(p => items.Select(i => i.ProductId).Contains(p.ProductId));
            result.CartId = cart.CartId;
            result.StoreName = store.Name;
            result.Address = store.Address;
            result.MemberId = cart.MemberId;
            
            // cartItem裡面的資料表
            // 在item裡面找到product
            // ProductOptionCard
            var productOptionCards = new List<ProductOptionCard>();
            var productOption = _repo.GetAll<ProductOption>().Where(pr => products.Select(p => p.ProductId).Contains(pr.ProductId));
            var productOptionDetail = _repo.GetAll<ProductOptionDetail>().Where(pod => productOption.Select(pr => pr.ProductOptionId).Contains(pod.ProductOptionId));

            foreach (var i in items)
            {
                //把屬於ITEM的 ITEMDETAIL抓出來
                var tempDetails = itemDetails.Where(id => id.ItemId == i.ItemId);

                var prod = products.FirstOrDefault(p => p.ProductId == i.ProductId);

                var tempProductOptions = productOption.Where(x => x.ProductId == i.ProductId);

                var tempProductOptionDetails = productOptionDetail.Where(x => tempDetails
                .Select(y => y.ProductOptionDetailId).Contains(x.ProductOptionDetailId));

                var pName = productOption.Where(p =>
                 tempProductOptionDetails.Where(x =>
                 tempDetails.Select(y => y.ProductOptionDetailId)
                .Contains(x.ProductOptionDetailId))
                .Select(x => x.ProductOptionId).Contains(p.ProductOptionId));
              

                var pTemp = tempDetails.Select(x => new ProductOptionCard
                {
                    ProductOptionId = tempProductOptionDetails.FirstOrDefault(y => y.ProductOptionDetailId == x.ProductOptionDetailId).ProductOptionId,
                    ProductOptionName = pName.FirstOrDefault(y => y.ProductOptionId == tempProductOptionDetails.FirstOrDefault(z => z.ProductOptionDetailId == x.ProductOptionDetailId).ProductOptionId).Name,
                    addPrice = tempProductOptionDetails.FirstOrDefault(y => y.ProductOptionDetailId == x.ProductOptionDetailId).AddPrice,
                    ProductOptionDetail = tempProductOptionDetails.FirstOrDefault(y => y.ProductOptionDetailId == x.ProductOptionDetailId).Name
                }).ToList(); 
            

                var temp = new CartItem()
                {
                    ItemId = i.ItemId,
                    UnitPrice = prod.UnitPrice,
                    ProductName = prod.Name,
                    Description = prod.Description,
                    ProductOptionCards = pTemp,
                    Quantity = i.Quantity,
                    TotalPrice = i.Quantity * prod.UnitPrice
                              
                };
                result.CartItems.Add(temp);
            }

            return result;             

        }
          
        
    }     

}