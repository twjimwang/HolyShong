using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.DBModels;

namespace HolyShong.Repositories
{
    public class HolyShongRepository
    {
        public HolyShongDBContext _ctx;

        public HolyShongRepository()
        {
            _ctx = new HolyShongDBContext();
        }

        public List<Member> GetMembers()
        {
            return _ctx.Member.ToList();            
        }

        public List<Address> GetAddress()
        {
            return _ctx.Address.ToList();
        }

        public List<Rank> GetRank()
        {
            return _ctx.Rank.ToList();
        }

    }
}