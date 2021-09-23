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

        public IQueryable<Member> GetMembers()
        {
            return _ctx.Member;
        }
        public IQueryable<Rank> GetRanks()
        {
            return _ctx.Rank;
        }
        public IQueryable<Address> GetAddresses()
        {
            return _ctx.Address;
        }
    }
}