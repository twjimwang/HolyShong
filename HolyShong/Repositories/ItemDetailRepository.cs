using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class ItemDetailRepository
    {
        public int ItemDetailId { get; set; }  
        public int ItemId { get; set; }  
        public int ProductOptionId { get; set; }  
        public int ProductOptionIdDetailId { get; set; }  
    }
}