using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;

namespace HolyShong.Services
{
    public class OrderService
    {
        private readonly HolyShongRepository _repo;
        public OrderService()
        {
            _repo = new HolyShongRepository();
        }

    }
}