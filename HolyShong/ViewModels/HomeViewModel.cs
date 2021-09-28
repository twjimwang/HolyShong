using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class HomeViewModel
    {
        public List<StoreCategory> StoreCategories { get; set; }
        public List<StoreCardBlock> StoreCardBlocks { get; set; }
    }

    public class StoreCategory
    {
        public int StoreCategoryId { get; set; }
        public string StoreCategoryImg { get; set; }
        public string StoreCategoryName { get; set; }
    }

    public class StoreCardBlock
    {
        public string StoreCategory { get; set; }
        public List<StoreCard> StoreCards { get; set; }
    }

    public class StoreCard
    {
        public string StoreImg { get; set; }
        public string StoreName { get; set; }
    }
}