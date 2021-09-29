using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Repositories;

namespace HolyShong.Services
{
    public class CartService
    {
        private readonly HolyShongRepository _repo;
        public CartService()
        {
            _repo = new HolyShongRepository();
        }

        public CartViewModel GetCartViewModel()
        {
            var result = new CartViewModel()
            {
                Items = new List<Item>()
                {
                    new Item (){ProductOptionCards = new List<ProductOptionCard>() }
                }   
            };
            return result;
        }


    }
     
}