using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models;

namespace HolyShong.Services
{
    public class MemberRegisterService
    {
        private readonly HolyShongRepository _repo;
        //private readonly DbContext _dbc;
        public MemberRegisterService() 
        {
            _repo = new HolyShongRepository();
        //   _dbc = new HolyShongContext();
        }

        public bool CreateAccount(MemberRegisterViewModel registerVM) 
        {
            if (registerVM == null)
            {
                return false;
            }

            //View Model -> Data Model, 並以HtmlEncode做安全性編碼
            Member account = new Member
            {
                MemberId = 104,
                FirstName = HttpUtility.HtmlEncode(registerVM.FirstName),
                LastName = HttpUtility.HtmlEncode(registerVM.LastName),
                //Password = HttpUtility.HtmlEncode(registerVM.Password),
                Password = HashService.MD5Hash(HttpUtility.HtmlEncode(registerVM.Password)),
                CreateTime = DateTime.UtcNow.AddHours(8),
                Email = HttpUtility.HtmlEncode(registerVM.Email),
                Cellphone = HttpUtility.HtmlEncode(registerVM.Cellphone),
                IsEnable = false,
                IsDelete = false,
                ActivetionCode = Guid.NewGuid(),
                EffectiveTime = DateTime.UtcNow.AddHours(11)
            };

            bool status = false;

            DbContext context = new HolyShongContext();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    _repo.Create<Member>(account);
                    _repo.SaveChange();
                    tran.Commit();
                    status = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }
            }
            return status;
        }
        
    }
}