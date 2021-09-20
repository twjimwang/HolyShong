using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class CartRepository
    {
        public int CartId { get; set; }
        public int MemberId { get; set; }
        public bool IsTablewares { get; set; }
        public bool IsBag { get; set; }
        public int DiscountMemberID { get; set; }
    }
}