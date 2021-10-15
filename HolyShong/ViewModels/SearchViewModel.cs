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
}