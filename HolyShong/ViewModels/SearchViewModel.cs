using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;

namespace HolyShong.ViewModels
{
    public class SearchViewModel
    {
        public int StoreCategoryId { get; set; }
        public string StoreCategoryName { get; set; }
        public List<StoreCard> StoreCards { get; set; }
    }

}