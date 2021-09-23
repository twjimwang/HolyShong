using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class MemberAndRankViewModel
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public int Zipcode { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

    public class MemberProfileViewModel
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string Cellphone { get; set; }
        public int? Zipcode { get; set; }
        public string Address { get; set; }
        public bool IsPrimary { get; set; }
        public string Email { get; set; }
        public string LevelName { get; set; }

        public string ShowPrimary()
        {
            if (IsPrimary)
                return "送送會員";
            else return "一般會員";

        }
    }
}