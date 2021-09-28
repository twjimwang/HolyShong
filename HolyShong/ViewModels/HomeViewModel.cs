using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class HomeViewModel
    {
        public int StoreCategoryId { get; set; }
        public string StoreCategoryName { get; set; }
        public string StoreCategoryImg { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreImg { get; set; }
    }
}