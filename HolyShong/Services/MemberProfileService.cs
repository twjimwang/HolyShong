using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class MemberProfileService
    {
        private readonly HolyShongRepository _repo ;
        public MemberProfileService()
        {
            _repo = new HolyShongRepository();
        }
        /// <summary>
        /// 取得特定ID的會員
        /// </summary>
        /// <returns></returns>
        public Member GetMember()
        {
            
            return _repo.GetAll<Member>().FirstOrDefault();
        }

        public Rank GetRankByMemberId(int id)
        {
            return _repo.GetAll<Rank>().FirstOrDefault(r=>r.MemberId==id);
        }



        //public List<MemberProfileViewModel> GetMemberProfileData(int? id)
        //{

        //    List<Member> members = _repo.GetMembers().ToList();
        //    List<Address> addresses = _repo.GetAddresses().ToList();
        //    List<Rank> ranks = _repo.GetRanks().ToList();

        //    var memberProfileVM =
        //        from m in members
        //        join a in addresses
        //        on m.MemberId equals a.MemberId
        //        join r in ranks
        //        on m.MemberId equals r.MemberId
        //        where m.MemberId == id
        //        select new MemberProfileViewModel
        //        {
        //            MemberId = m.MemberId,
        //            FullName = m.LastName + m.FirstName,
        //            Cellphone = m.Cellphone,
        //            Zipcode = a.ZipCode,
        //            Address = a.Address1,
        //            IsPrimary = r.IsPrimary,
        //            Email = m.Email
        //        };
        //    return (List<MemberProfileViewModel>)memberProfileVM.ToList();
        //}

        //public List<MemberProfileViewModel> GetAllMemberProfile()
        //{
        //    var result = new List<MemberProfileViewModel>();
        //    var members = _repo.GetMembers().ToList();
        //    var addresses = _repo.GetAddresses()
        //        .Where(a => members.Select(m => m.MemberId).Contains(a.MemberId)).ToList();
        //    var ranks = _repo.GetRanks().Where(r => members.Select(m => m.MemberId).Contains(r.MemberId)).ToList();

        //    foreach (var m in members)
        //    {
        //        var address = addresses.FirstOrDefault(a => a.MemberId == m.MemberId);
        //        var rank = ranks.First(r => r.MemberId == m.MemberId);
        //        var temp = new MemberProfileViewModel
        //        {
        //            MemberId = m.MemberId,
        //            FullName = $"{m.LastName},{m.FirstName}",
        //            Cellphone = m.Cellphone,
        //            Zipcode = address?.ZipCode,
        //            Address = address?.Address1,
        //            IsPrimary = rank.IsPrimary,
        //            LevelName = rank.IsPrimary ? "尊貴會員" : "一般會員",
        //            Email = m.Email
        //        };
        //        result.Add(temp);

        //    }
            //return result;





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
        //}
    }
}