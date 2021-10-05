using System.Data.Entity;
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
        private readonly HolyShongRepository _repo;
        public MemberProfileService()
        {
            _repo = new HolyShongRepository();
        }

        //取得特定會員Id的Member資料表
        public Member GetMember(int id)
        {
            return _repo.GetAll<Member>().FirstOrDefault(x => x.MemberId == id);
        }

        //取得特定會員Id的Rank資料表
        public Rank GetRankByMemberId(int id)
        {
            return _repo.GetAll<Rank>().FirstOrDefault(x => x.MemberId == id);
        }

        //建立ViewModel
        public UserProfileViewModel GetMemberProfileViewModel(int id)
        {
            //待處理=>
            var member = GetMember(id);
            bool primary = false;
            Rank rank = GetRankByMemberId(id);
            if (rank != null && rank.IsPrimary)
            {
                primary = true;
            }

            var result = new UserProfileViewModel()
            {
                MemberId = member.MemberId,
                Cellphone = member.Cellphone,
                Email = member.Email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                IsPrimary = primary
            };

            if (rank != null && rank.EndTime != null && rank.EndTime.HasValue)
            {
                //會是資料庫的日期減一天(因為資料庫的結束時間是該日的0:00)
                result.PrimaryEndTime = ((DateTime)rank.EndTime).AddDays(-1).ToShortDateString();
            }
            return result;
        }

        public bool EditMemberProfile(UserProfileViewModel memberProfileViewModel)
        {
            DbContext context = new HolyShongContext();
            var member = GetMember(memberProfileViewModel.MemberId);
            member.LastName = memberProfileViewModel.LastName;
            member.FirstName = memberProfileViewModel.FirstName;
            member.Cellphone = memberProfileViewModel.Cellphone;
            member.UpdateTime = DateTime.UtcNow.AddHours(8);
            //member.Email = memberProfileViewModel.Email;           
            var rank = GetRankByMemberId(memberProfileViewModel.MemberId);
            //if (rank != null)
            //{
            //    rank.IsPrimary = memberProfileViewModel.IsPrimary;
            //}
            //else
            //{
            //    var newRank = new Rank()
            //    {
            //        RankId = 100,
            //        IsPrimary = false,
            //        MemberId = memberProfileViewModel.MemberId,
            //    };

            //    using (var transaction = context.Database.BeginTransaction())
            //        try
            //        {
            //            _repo.Create<Rank>(rank);
            //            _repo.SaveChange();
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            transaction.Rollback();
            //        }
            //}
            using (var transaction = context.Database.BeginTransaction())
                try
                {
                    _repo.Update<Member>(member);
                    if(rank != null)
                    {
                    _repo.Update<Rank>(rank);
                    }
                    _repo.SaveChange();
                    transaction.Commit();
                    return (true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false);
                }
        }


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
        //    return result;





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