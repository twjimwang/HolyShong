using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class DiscountService
    {
        DbContext context = new HolyShongContext();
        private readonly HolyShongRepository _repo;
        public DiscountService()
        {
            _repo = new HolyShongRepository();
        }


        /// <summary>
        /// 結帳時使用優惠模組
        /// </summary>
        /// <returns></returns>
        public decimal UseDiscount(string discountName)
        {

            //有領用此優惠模組，核對1. 有無使用過 2.條件是否吻合 並使用

            //無領用，先領用，在核對以上條件
            throw new ArgumentException();
        }

        /// <summary>
        /// 漢堡處領用優惠卷
        /// </summary>
        /// <returns></returns>
        public string AcquireDiscount(string discountName)
        {
            var memberId = 1;
            //找到此優惠卷ID，且仍在效期
            var discountId = _repo.GetAll<Discount>().FirstOrDefault(d => d.DisplayName == discountName && d.EndTime<= DateTime.Now).DiscountId;
            if(discountId == 0)
            {
                //輸入邀請好友
                var order = _repo.GetAll<Order>().Where(o => o.MemberId == memberId);
                if(order.Count() == 0)
                {
                    var displayName = discountName.Split('-')[0].ToUpper();
                    if(discountName == "SHARE")
                    {
                        var shareMember = Int32.Parse(discountName.Split('-')[1]);
                        var findShare = _repo.GetAll<Member>().FirstOrDefault(m => m.MemberId == shareMember);
                    }

                }
                
                return "折扣碼錯誤，找不到優惠卷";
            }
            //判斷此會員有無領用過
            var haveDiscount = _repo.GetAll<DiscountMember>().Where(dm=>dm.MemberId == memberId && dm.DiscountId == discountId);

            //有，傳回已領用過
            if (haveDiscount != null)
            {
                return "優惠卷已使用";
            }

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    //沒有，加入資料庫(DiscountMember && Discount)
                    DiscountMember discountMember = new DiscountMember()
                    {
                        DiscountId = discountId,
                        MemberId = memberId,
                        IsUsed = false
                    };

                    //Discount 確認還有沒有額度，沒有要減一
                    var discount = _repo.GetAll<Discount>().FirstOrDefault(d => d.DiscountId == discountId);
                    if(discount.UseLimit != null && discount.UseLimit > 0)
                    {
                        discount.UseLimit -= 1;
                        _repo.Update(discount);
                    }
                    _repo.Create(discountMember);
                    _repo.SaveChange();
                    tran.Commit();

                    return "完成新增";
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    return ex.ToString();
                }
            }

        }

        /// <summary>
        /// 會員的所有有效優惠卷
        /// </summary>
        /// <returns></returns>
        public List<DiscountViewModel> GetDiscountByMemberId()
        {
            List<DiscountViewModel> discountVM = new List<DiscountViewModel>();
            //memberId
            var memberId = 1;
            var memberDiscounts = _repo.GetAll<DiscountMember>().Where(dm => dm.MemberId == memberId);
            if(memberDiscounts == null)
            {
                return discountVM;
            }

            //找出還有效的discount
            discountVM = _repo.GetAll<Discount>().Where(d => memberDiscounts.Select(md => md.DiscountId).Contains(d.DiscountId)).Where(md=> md.EndTime >= DateTime.Now).Select(d=> new DiscountViewModel() {
                DiscountMemberId = memberDiscounts.FirstOrDefault(md=>md.DiscountId == d.DiscountId).DiscountMemberId,
                DiscountName = d.DisplayName,
                EndTime =d.EndTime.Value
            }).ToList();

            return discountVM;

        }
    }
}