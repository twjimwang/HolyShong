using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class CartViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public bool IsTablewares { get; set; }

        public bool IsPlasticbag { get; set; }

        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem : StoreProduct
    {

    }
}