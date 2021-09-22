using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HolyShong.Repositories
{
    public class StoreRepository
    {
        public int StoreId { get; set; }
        public int StoreCategoryId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string KeyWord { get; set; }
        public string Address { get; set; }
        public string Cellphone { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDelete { get; set; }
        public int? MemberId { get; set; }
    }
}