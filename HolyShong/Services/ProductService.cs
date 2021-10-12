using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{

    public class ProductService
    {
        private readonly HolyShongRepository _productRespository;

        public ProductService()
        {
            _productRespository = new HolyShongRepository();

        }
  
    }
}