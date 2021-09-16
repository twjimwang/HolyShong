using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Models;
using HolyShong.DBModels;
using HolyShong.ViewModels;
using HolyShong.Repositories;

namespace HolyShong.Services
{
    public class ProfileServices
    {
        //初始化DATA
        public HolyShongRepository _holyShongRepository;
        public ProfileServices()
        {
            _holyShongRepository = new HolyShongRepository();
        }

        public List<MemberProfileViewModel> GetMemberProfileData()
        {
            //Data Model
            List<Member> members = _holyShongRepository.GetMembers();
            List<Address> addresses = _holyShongRepository.GetAddress();
            List<Rank> ranks = _holyShongRepository.GetRank();


            var memberAndAddress =
                from m in members
                join a in addresses
                on m.MemberId equals a.MemberId
                select new MemberAndRankViewModel
                {
                    MemberId = m.MemberId,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Cellphone = m.Cellphone,
                    Zipcode = a.ZipCode,
                    Address = a.Address1,
                    Email = m.Email,
                };

            var memberProfileVM =
                from m in memberAndAddress
                join r in ranks
                on m.MemberId equals r.MemberId
                select new MemberProfileViewModel
                {
                    MemberId = m.MemberId,
                    FullName = m.FirstName + m.LastName,
                    Cellphone = m.Cellphone,
                    Zipcode = m.Zipcode,
                    Address = m.Address,
                    Email = m.Email,
                    IsPrimary = r.IsPrimary
                };
            return (List<MemberProfileViewModel>)memberProfileVM.ToList();
        }
    }
}