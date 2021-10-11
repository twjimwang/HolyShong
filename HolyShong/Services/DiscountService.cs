using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class DiscountService
    {

        private readonly HolyShongRepository _repo;
        public DiscountService()
        {
            _repo = new HolyShongRepository();
        }


        /// <summary>
        /// 結帳時使用優惠模組
        /// </summary>
        /// <returns></returns>
        public decimal UseDiscount()
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
            //找到此優惠卷ID
            var discountId = _repo.GetAll<Discount>().FirstOrDefault(d => d.DisplayName == discountName).DiscountId;
            if(discountId == 0)
            {
                return "折扣碼錯誤，找不到優惠卷";
            }
            //判斷此會員有無領用過
            var memberId = 1;
            var haveDiscount = _repo.GetAll<DiscountMember>().Where(dm=>dm.MemberId == memberId && dm.DiscountId == discountId);

            //有，傳回已領用過
            if (haveDiscount != null)
            {
                return "優惠卷已使用";
            }

            //沒有，加入資料庫(DiscountMember && Discount)
            DiscountMember discountMember = new DiscountMember()
            {
                DiscountId = discountId,
                MemberId = memberId,
                IsUsed = false
            };

            //Discount 要減一


            _repo.Create(discountMember);
            _repo.SaveChange();

            return "完成新增";

        }


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

            discountVM = _repo.GetAll<Discount>().Where(d => memberDiscounts.Select(md => md.DiscountId).Contains(d.DiscountId)).Select(d=> new DiscountViewModel() {
                DiscountMemberId = memberDiscounts.First(md=>md.DiscountId == d.DiscountId).DiscountMemberId,
                DiscountName = d.DisplayName,
                EndTime =d.EndTime
            }).ToList();

            return discountVM;

        }
    }
}