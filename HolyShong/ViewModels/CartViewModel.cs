using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        public string CustomerAddress { get; set; }

        public bool IsTablewares { get; set; }

        public bool IsPlasticbag { get; set; }

        public DateTime CreatedDate { get; set; }


        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem : StoreProduct
    {

    }
}