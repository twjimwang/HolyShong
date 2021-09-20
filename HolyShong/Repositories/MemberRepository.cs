using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Repositories
{
    public class MemberRepository
    {
        public int  MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDelete { get; set; }
    }
}