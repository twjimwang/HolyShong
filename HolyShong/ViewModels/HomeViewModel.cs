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
        public int[] NumberArray { get; set; }
    }
    //主分類區
    public class StoreCategory
    {
        public int StoreCategoryId { get; set; }
        public string StoreCategoryImg { get; set; }
        public string StoreCategoryName { get; set; }
    }


    //主頁面區
    public class StoreCardBlock
    {
        public int StoreCategoryId { get; set; }
        public string StoreCategoryImg { get; set; }
        public string StoreCategoryName { get; set; }
        //主頁面區
        public List<StoreCard> StoreCards { get; set; }
    }
    //卡片元素
    public class StoreCard
    {
        public int StoreId { get; set; }
        public string StoreImg { get; set; }
        public string StoreName { get; set; }

        public string ProductCategoryName { get; set; }
    }
}