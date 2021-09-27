﻿using System;
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
            decimal total = 0;  //計算訂單總金額
            OrderDeliverViewModel orderResult = new OrderDeliverViewModel()
            {
                OderLists = new List<OrderList>()
            };

            var order = _repo.GetAll<Order>().FirstOrDefault(o => o.OrderId == orderId);
            if(order == null)
            {
                return orderResult;
            }

            var deliver = _repo.GetAll<Deliver>().First(d => d.DeliverId == order.DeliverId);
            var member = _repo.GetAll<Member>().First(m => m.MemberId == deliver.MemberId);
            var store = _repo.GetAll<Store>().First(s => s.StoreId == order.StoreId);
            var orderDetail = _repo.GetAll<OrderDetail>().Where(od => od.OrderId == order.OrderId);
            var product = _repo.GetAll<Product>().Where(p => orderDetail.Select(od=>od.ProductId).Contains(p.ProductId));

            var productlist = orderDetail.Select(od => new OrderList
            {
                ProductName = product.FirstOrDefault(p=>p.ProductId == od.ProductId).Name,
                ProductPrice = od.UnitPrice,
                ProductQuantity = od.Quantity
            }).ToList();

            foreach(var p in productlist)
            {
                total += p.ProductPrice * p.ProductQuantity;
            }

            orderResult.DeliverName = member.LastName + member.FirstName;
            orderResult.DeliverImg = deliver.HeadshotImg;
            orderResult.CustomerAddress = order.DeliveryAddress;
            orderResult.CustomerNotes = order.Notes;
            orderResult.RestaurantName = store.Name;
            orderResult.RestaurantAddress = store.Address;
            orderResult.OderLists = productlist;
            orderResult.Total = total;
            return orderResult;
        }

        /// <summary>
        /// 查詢會員歷史訂單
        /// </summary>
        public List<OrderListViewModel> GetOrderByMemberId(int memberId)
        {
            decimal total = 0;

            var member = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == memberId);
            if(member == null)
            {
                return new List<OrderListViewModel>();
            }

            var order = _repo.GetAll<Order>().Where(o => o.MemberId == member.MemberId);
            var deliver = _repo.GetAll<Deliver>().Where(d => order.Select(o=>o.OrderId).Contains(d.DeliverId));
            var store = _repo.GetAll<Store>().Where(s => order.Select(o => o.StoreId).Contains(s.StoreId));
            var orderDetail = _repo.GetAll<OrderDetail>().Where(od => order.Select(o => o.OrderId).Contains(od.OrderId));
            var product = _repo.GetAll<Product>().Where(p => orderDetail.Select(od => od.ProductId).Contains(p.ProductId));
            var productOption = _repo.GetAll<ProductOption>().Where(po => orderDetail.Select(od => od.ProductOptionId).Contains(po.ProductOptionId));
            var productOptionDetail = _repo.GetAll<ProductOptionDetail>().Where(pod => orderDetail.Select(od=>od.ProductOptionDetailId).Contains(pod.ProductOptionDetailId));

            //產品細節
            var tempOption = orderDetail.Select(od => new OrderOptionDetail
            {
                ProdectOptionName = productOptionDetail.Select(pod => pod.Name).ToString()
            }).ToList();

            //產品
            var tempProduct = orderDetail.Select(od => new OrderProduct
            {
                ProductName = product.First(p => p.ProductId == od.ProductId).Name,
                ProductPrice = od.UnitPrice,
                ProductQuantity = od.Quantity,
                OrderOptions = tempOption
            }).ToList();

            foreach(var p in tempProduct)
            {
                total += p.ProductPrice * p.ProductQuantity;
            }

            var temp = order.Select(o => new OrderListViewModel
            {
                DeliverDate = o.RequiredDate == null ? o.CreateDate : o.RequiredDate,
                RestaurantName = store.First(s => s.StoreId == o.OrderId).Name,
                OrderLists = tempProduct,

            }).ToList();

            return temp;
        }

    }
}








////取得表單
//var orders = _repo.GetAll<Order>().Where(o => o.OrderId == orderId);
//var delivers = _repo.GetAll<Deliver>().Where(d => orders.Select(o => o.DeliverId).Contains(d.DeliverId)).Distinct();
//var members = _repo.GetAll<Member>().Where(m => delivers.Select(d => d.MemberId).Contains(m.MemberId));
//var stores = _repo.GetAll<Store>().Where(s => orders.Select(o => o.StoreId).Contains(s.StoreId));
//var orderDetails = _repo.GetAll<OrderDetail>().Where(od => orders.Select(o => o.OrderId).Contains(od.OrderId));
//var orderProducts = _repo.GetAll<Product>().Where(p => orderDetails.Select(od => od.ProductId).Contains(p.ProductId));

////傳入VM
//orderResult = orders.Select(o => new OrderDeliverViewModel
//{
//    DeliverName = members.First(m => m.MemberId == d)
//});