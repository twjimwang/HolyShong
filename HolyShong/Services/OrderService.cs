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
        public OrderDeliverViewModel GetOrder(int orderId)
        {
            orderId = 1; 
            decimal total = 0;  //計算訂單總金額
            OrderDeliverViewModel orderResult = new OrderDeliverViewModel()
            {
                OderLists = new List<OrderList>()
            };
            //取得表單
            var order = _repo.GetAll<Order>().Where(o => o.OrderId == orderId);
            var deliver = _repo.GetAll<Deliver>().Where(d => d.DeliverId = );

            //傳入VM






            //取得該訂單商品明細
            order.OderLists = _repo.GetAll<OrderDetail>().Where(od => od.OrderId == orderId).Select(od => new OrderList
            {
                ProductName = _repo.GetAll<Product>().First(p => p.ProductId == od.ProductId).Name,
                ProductQuantity = od.Quantity,
                ProductPrice = od.UnitPrice,
            }).ToList();

            order = _repo.GetAll<Order>().Where(o => o.OrderId == orderId).Select(o => new OrderDeliverViewModel 
            {
                DeliverName = ,
                
            

            return null;
        }

    }
}