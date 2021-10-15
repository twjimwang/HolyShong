using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HolyShong.Models.HolyShongModel;
using HolyShong.Repositories;
using HolyShong.ViewModels;
using HolyShong.Models;
using System.Net.Mail;
using System.Net;

namespace HolyShong.Services
{
    public class MemberService
    {
        private readonly HolyShongRepository _repo;
        //private readonly DbContext _dbc;
        public MemberService() 
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
                MemberId = 109,
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

            //var GenarateUserVerificationLink = "/Register/UserVerification/" + account.ActivetionCode;
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("holyshong2022@gmail.com"); // set your email    
            var fromEmailpassword = "Hs20211014"; // Set your password     
            var toEmail = new MailAddress(account.Email);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "註冊完成!!";
            Message.Body =
            "<br/> 你成功註冊了一個帳號!" +
            "<br/> 請點擊下方連結來登入會員!";
            
            //+"<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            try
            {
                smtp.Send(Message);
                Message.Dispose(); //釋放資源
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            

            return status;
        }

        public Member UserLogin(MemberLoginViewModel loginVM)
        {
            //使用HtmlEncode將帳密做HTML編碼, 去除有害的字元
            string name = HttpUtility.HtmlEncode(loginVM.Email);
            //string password = HttpUtility.HtmlEncode(loginVM.Password);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.Password));

            Member user = _repo.GetAll<Member>()
              .Where(x => x.Email.ToUpper() == name.ToUpper() && x.Password == password && x.IsEnable == true)
              .SingleOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }

    }
}