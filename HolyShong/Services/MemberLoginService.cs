using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models;
using HolyShong.Models.HolyShongModel;


namespace HolyShong.Services
{
    public class MemberLoginService
    {
        private readonly HolyShongRepository _repo;

        public MemberLoginService() 
        {
            _repo = new HolyShongRepository();
        }

        public MemberRegistrViewModel GetAllMember()
        {
            var result = new MemberRegistrViewModel()
            {
                ProductCards = new List<ProductCard>(),
                Title = "歡迎光臨"
            };
            var products = _repo.GetAll().ToList();
            foreach (var prod in products)
            {
                var category = _repo.GetAll()
                    .FirstOrDefault(x => x.CategoryId == prod.CategoryId);
                var temp = new ProductCard
                {
                    ProductId = prod.ProductId,
                    Name = prod.Name,
                    ImgUrl = prod.Img,
                    CategoryName = category == null ? "預設類別" : category.Name
                };
                result.ProductCards.Add(temp);
            }

            result.Title = "NONO";

            return result;
        }

    }
}