using HolyShong.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class RestaurantService
    {
        private readonly HolyShongRepository _repo;
        public RestaurantService()
        {
            _repo = new HolyShongRepository();
        }
        
    }
}