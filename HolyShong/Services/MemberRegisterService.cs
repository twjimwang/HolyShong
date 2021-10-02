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
        private readonly DbContext _dbc;
        public MemberRegisterService() 
        {
            _repo = new HolyShongRepository();
           _dbc = new HolyShongContext();
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
                FirstName = HttpUtility.HtmlEncode(registerVM.FirstName),
                LastName = HttpUtility.HtmlEncode(registerVM.LastName),
                Password = HttpUtility.HtmlEncode(registerVM.Password),
                //Password = HashService.MD5Hash(HttpUtility.HtmlEncode(registerVM.Password)),
                Email = HttpUtility.HtmlEncode(registerVM.Email),
                Cellphone = HttpUtility.HtmlEncode(registerVM.Cellphone)
            };

            bool status = false;


            using (var tran = _dbc.Database.BeginTransaction())
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