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

        public CartViewModel GetCartViewModel(int Id)
        {
            var result = new CartViewModel()
            {
                CartItems = new List<CartItem>()
                {
                    new CartItem (){ProductOptionCards = new List<ProductOptionCard>()}
                }   
            };
            // 代入id
            var itemResult = _repo.GetAll<Item>().FirstOrDefault(x=>x.ProductId == Id);
            if (itemResult == null)
            {
                return result;
            }

            var product = _repo.GetAll<Product>().Where(x=>x.ProductId == Id).ToList();
            var CartViewModel = product.Select(p => new CartViewModel
            {
                StoreName = p.Name,
              

            }).ToList();
            
           
            return result;
            
        }
      
      
           
           
       

    }
     
}