using HolyShong.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.ViewModels;
using HolyShong.Models;
using HolyShong.DBmodels;

namespace HolyShong.Services
{
    public class ProfileService
    {
        public HolyShongRepository _holyShongRepository;
        public ProfileService()
        {
            _holyShongRepository = new HolyShongRepository();
        }

        public List<MemberProfileViewModel> GetMemberProfileData()
        {
            List<Member> members = _holyShongRepository.GetMembers();
            List<Address> addresses = _holyShongRepository.GetAddresses();
            List<Rank> ranks = _holyShongRepository.GetRanks();

            var memberProfileVM =
                from m in members
                join a in addresses
                on m.MemberId equals a.MemberId
                join r in ranks
                on m.MemberId equals r.MemberId
                select new MemberProfileViewModel
                {
                    MemberId = m.MemberId,
                    FullName = m.LastName + m.FirstName,
                    Cellphone = m.Cellphone,
                    Zipcode = a.ZipCode,
                    Address = a.Address1,
                    IsPrimary = r.IsPrimary,
                    Email = m.Email
                };
            return (List<MemberProfileViewModel>)memberProfileVM.ToList();
        }
    }
}