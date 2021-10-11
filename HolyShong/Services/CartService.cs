using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Repositories;
using HolyShong.Models.HolyShongModel;

using System.Web.Services;


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
           
            //找item裡面找到包含上面篩選出來的cart的cartId
            var items = _repo.GetAll<Item>().Where(i => i.CartId == cart.CartId);
            var itemDetails = _repo.GetAll<ItemDetail>().Where(id => items.Select(i => i.ItemId).Contains(id.ItemId));
            var products = _repo.GetAll<Product>().Where(p => items.Select(i => i.ProductId).Contains(p.ProductId));
            result.CartId = cart.CartId;
            result.StoreName = store.Name;
            result.Address = store.Address;
            
            //cartItem裡面的資料表
            //在item裡面找到product
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

                var pTemp = tempDetails.Select(x => new ProductOptionCard
                {
                    ProductOptionId = x.ProductOptionId,
                    ProductOptionName = tempProductOptions.FirstOrDefault(y => y.ProductOptionId == x.ProductOptionId).Name,
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
                    TotalPrice = i.Quantity * prod.UnitPrice
                              
                };
                result.CartItems.Add(temp);
            }

            return result;

            //foreach(var id in itemDetails)
            //{
            //    var pTemp = new ProductOptionCard()
            //    {
            //        ProductOptionId = id.ProductOptionId,
            //        ProductOptionName = productOption.FirstOrDefault(po=>po.ProductOptionId == id.ProductOptionId).Name,
            //        ProductOptionDetail = productOptionDetail.FirstOrDefault(pod=>pod.ProductOptionDetailId == id.ProductOptionDetailId).Name,
            //        addPrice = productOptionDetail.FirstOrDefault(pod=>pod.ProductOptionDetailId == id.ProductOptionDetailId).AddPrice,
            //    };
            //    productOptionCards.Add(pTemp);
            //}

            

        }
           
        

    }     

}