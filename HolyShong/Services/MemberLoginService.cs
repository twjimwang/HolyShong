using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models;
using HolyShong.Models.HolyShongModel;


namespace HolyShong.Services
{
    public class MemberLoginService
    {
        private readonly HolyShongRepository _repo;

        public MemberLoginService() 
        {
            _repo = new HolyShongRepository();
        }

        public Member UserLogin(MemberLoginViewModel loginVM)
        {
            //使用HtmlEncode將帳密做HTML編碼, 去除有害的字元
            string name = HttpUtility.HtmlEncode(loginVM.Email);
            //string password = HttpUtility.HtmlEncode(loginVM.Password);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.Password));

            Member user = _repo.GetAll<Member>()
              .Where(x => x.Email.ToUpper() == name.ToUpper() && x.Password == password)
              .SingleOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }
        
    }
}