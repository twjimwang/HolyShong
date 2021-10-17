using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class DiscountViewModel
    {
        public string DiscountName { get; set; }
        public DateTime EndTime { get; set; }

        public int DiscountMemberId { get; set; }
    }
}