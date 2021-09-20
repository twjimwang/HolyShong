using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Repositories
{
    public class RankRepository
    {
        public int RankId { get; set; }
        public int MemberId { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime EndTime { get; set; }

    }
}