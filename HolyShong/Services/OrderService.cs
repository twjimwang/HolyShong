using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models.HolyShongModel;

namespace HolyShong.Services
{
    public class OrderService
    {
        private readonly HolyShongRepository _repo;
        public OrderService()
        {
            _repo = new HolyShongRepository();
        }

        /// <summary>
        /// 單筆外送訂單呈現
        /// </summary>
        /// <returns></returns>
        public OrderDeliverViewModel GetOrder()
        {
            var orderId = 1;
            OrderDeliverViewModel order = new OrderDeliverViewModel()
            {
                OderLists = new List<OrderList>()
            };

            //取得該訂單商品明細
            order.OderLists = _repo.GetAll<Order>().Where();
            

            return null;
        }

    }
}