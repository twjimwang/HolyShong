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

        public List<MemberProfileViewModel> GetMemberProfileData(int? id)
        {

            List<Member> members = _holyShongRepository.GetMembers().ToList();
            List<Address> addresses = _holyShongRepository.GetAddresses().ToList();
            List<Rank> ranks = _holyShongRepository.GetRanks().ToList();

            var memberProfileVM =
                from m in members
                join a in addresses
                on m.MemberId equals a.MemberId
                join r in ranks
                on m.MemberId equals r.MemberId
                where m.MemberId == id
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

        public List<MemberProfileViewModel> GetAllMemberProfile()
        {
            var result = new List<MemberProfileViewModel>();
            var members = _holyShongRepository.GetMembers().ToList();
            var addresses = _holyShongRepository.GetAddresses()
                .Where(a => members.Select(m => m.MemberId).Contains(a.MemberId)).ToList();
            var ranks = _holyShongRepository.GetRanks().Where(r => members.Select(m => m.MemberId).Contains(r.MemberId)).ToList();
            
            foreach(var m in members)
            {
                var address = addresses.FirstOrDefault(a => a.MemberId == m.MemberId);
                var rank = ranks.First(r => r.MemberId == m.MemberId);
                var temp = new MemberProfileViewModel 
                {
                    MemberId = m.MemberId,
                    FullName = $"{m.LastName},{m.FirstName}",
                    Cellphone = m.Cellphone,
                    Zipcode = address?.ZipCode,
                    Address = address?.Address1,
                    IsPrimary = rank.IsPrimary,
                    LevelName = rank.IsPrimary ? "尊貴會員" : "一般會員",
                    Email = m.Email
                };
                result.Add(temp);

            }

            return result;
            //return members.Select(m => new MemberProfileViewModel
            //{
            //    MemberId = m.MemberId,
            //    FullName = $"{m.LastName},{m.FirstName}",
            //    Cellphone = m.Cellphone,
            //    Zipcode = addresses.FirstOrDefault(a => a.MemberId == m.MemberId)?.ZipCode,
            //    Address = addresses.FirstOrDefault(a => a.MemberId == m.MemberId)?.Address1,
            //    IsPrimary = ranks.First(r => r.MemberId == m.MemberId).IsPrimary,
            //    LevelName = ranks.First(r => r.MemberId == m.MemberId).IsPrimary?"尊貴會員":"一般會員",
            //    Email = m.Email
            //}).ToList();
        }
    }
}