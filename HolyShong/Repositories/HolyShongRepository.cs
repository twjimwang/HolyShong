using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.DBmodels;

namespace HolyShong.Repositories
{
    public class HolyShongRepository
    {
        public HolyShongDB _ctx;
        public HolyShongRepository()
        {
            _ctx = new HolyShongDB();
        }

        public List<Member> GetMembers()
        {
            return _ctx.Member.ToList();
        }
        public List<Rank> GetRanks()
        {
            return _ctx.Rank.ToList();
        }
        public List<Address> GetAddresses()
        {
            return _ctx.Address.ToList();
        }
    }
}