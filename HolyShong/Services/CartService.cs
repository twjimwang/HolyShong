using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Repositories;
using HolyShong.Models.HolyShongModel;

namespace HolyShong.Services
{
    public class CartService
    {
        private readonly HolyShongRepository _repo;
        public CartService()
        {
            _repo = new HolyShongRepository();
        }

        public CartViewModel GetCartViewModel(int cartId)
        {
            var result = new CartViewModel()
            {
                CartItems = new List<CartItem>()
                {
                    new CartItem (){ProductOptionCards = new List<ProductOptionCard>()}
                }   
            };
            // 代入id
            var CartResult = _repo.GetAll<Cart>().FirstOrDefault(x=>x.CartId == cartId);
            if (CartResult == null)
            {
                return result;
            }

            var StoreResult = _repo.GetAll<Store>().FirstOrDefault();
           

            return result;
        }
      
      
           
           
       

    }
     
}