using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class CartViewModel
    {
        public string StoreName { get; set; }
        public string ProductName { get; set; } 
        public string ProductOption { get; set; }
        public string ProductOptionDetail { get; set; }
        public bool IsTableWares { get; set; }
        public bool IsBag { get; set; }

    }
}