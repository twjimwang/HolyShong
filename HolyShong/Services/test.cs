using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.Models.HolyShongModel;

namespace HolyShong.Services
{
    public class test
    {
        private readonly HolyShongRepository _repo;
        public test()
        {
            _repo = new HolyShongRepository();
        }

        public Product GetProducts()
        {
            var result = _repo.GetAll<Product>().FirstOrDefault();
            return result;
        }
    }
}