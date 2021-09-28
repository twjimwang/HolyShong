using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    /// <summary>
    /// 會員頁面
    /// 用於呈現會員個人資料
    /// 使用資料表：
    /// Member
    /// Rank
    /// Address
    /// </summary>
    public class MemberProfileViewModel
    {

        /// <summary>
        /// 會員編號
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 會員姓氏
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 會員名字
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 會員手機
        /// </summary>
        public string Cellphone { get; set; }

        /// <summary>
        /// 會員郵遞區號
        /// </summary>
        public int Zipcode { get; set; }

        /// <summary>
        /// 行政區
        /// e.g.台北市大安區
        /// </summary>
        public string ZipcodeDistrict { get; set; }

        /// <summary>
        /// 會員地址
        /// 對應Address表
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 送送會員
        /// 對應Rank表
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// 會員信箱
        /// </summary>
        public string Email { get; set; }

        public Dictionary<int, string> CreateZipcodeDistrictList()
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>()
            {

            };
            return keyValuePairs;
        }

    }

}

