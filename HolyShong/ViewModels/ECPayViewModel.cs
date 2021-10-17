using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class ECPayViewModel
    {


        public List<Item> ListItems { get; set; }
    }

    /// <summary>
    /// 購買商品明細
    /// </summary>
    public class Item
    {
        //產品名稱
        public string Name { get; set; }
        //商品單價
        public decimal Price { get; set; }
        //幣別單位，固定為新台幣
        public string Currency { get; set; }
        //購買數量
        public int Quantity { get; set; }
        //商品說明網址，固定為空值
        public string URL { get; set; }
    }
}