using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class SearchViewModel
    {
        public int StoreCategoryId { get; set; }
        public List<StoreCard> StoreCards { get; set; }
    }

    public class SearchRequest
    {
        public string Keyword { get; set; }
        public int SearchCount { get; set; }
        public int Type { get; set; }
        public string Price { get; set; }
        public int DeliveryFee { get; set; }
    }
}