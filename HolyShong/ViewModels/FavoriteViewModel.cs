using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class FavoriteViewModel
    {
        public int StoreId { get; set; }      
        public DateTime CreateTime { get; set; }
        public List<FavoriteStore> favoriteStores { get; set; }
    }
    public class FavoriteStore
    {
        public string StoreImg { get; set; }
        public string StoreName { get; set; }
        public decimal DeliveryFee { get; set; }
    }
}