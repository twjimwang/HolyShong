using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Repositories
{
    public class AdressReposItory
    {
        public int AddressId { get; set; }

        public int MemberId { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int ZipCode { get; set; }

        
        public string Address1 { get; set; }

       
    }
}