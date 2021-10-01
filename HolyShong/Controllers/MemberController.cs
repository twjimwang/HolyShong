using HolyShong.Models;
using HolyShong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HolyShong.Repositories;
using HolyShong.Services;
using HolyShong.Models.HolyShongModel;
using System.Web.Security;

namespace HolyShong.Controllers
{
    public class MemberController : Controller
    {
        private readonly HolyShongRepository _repo;
        private readonly MemberLoginService _memberLoginService;

        public MemberController()
        {
            _repo = new HolyShongRepository();
            _memberLoginService = new MemberLoginService();
        }
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Eatpass()
        {
            return View();
        }
        /// <summary>
        /// 會員個人資料頁面
        /// 讀取資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserProfile(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {       
            var model = _profileService.GetMemberProfileViewModel((int)id);
                if(model == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            return View(model);
            }
        }
        /// <summary>
        /// 會員個人資料頁面
        /// 修改資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserProfile(MemberProfileViewModel memberProfileViewModel)
        {

            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult DeliverRegister()
        {
            return View();
        }

        public ActionResult Favorite()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult RestaurantRegister()
        {
            return View();
        }

        public ActionResult Invite()
        {
            return View();
        }
        /// <summary>
        /// 註冊頁面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(MemberRegistrViewModel registerViewModel)
        {
            return View();
        }

        /// <summary>
        /// 登入頁面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MemberLoginViewModel loginVM)
        {
            //一.未通過驗證
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            //二.通過Model驗證後, 使用HtmlEncode將帳密做HTML編碼, 去除有害的字元
            string name = HttpUtility.HtmlEncode(loginVM.Email);
            string password = HttpUtility.HtmlEncode(loginVM.Password);
            //string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.Password));

            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表記錄

            Member user = _memberLoginService.UserLogin(loginVM);
            //Account user = _ctx.Accounts.Where(x => x.Name.ToUpper() == name.ToUpper() && x.Password == password).FirstOrDefault();

            //Account user2 = _ctx.Accounts.SingleOrDefault(x => x.Name.ToUpper() == name.ToUpper() && x.Password == password);
            //找不到則彈回Login頁
            if (user == null)
            {
                ModelState.AddModelError("Password", "無效的帳號或密碼!");

                return View(loginVM);
            }

            //四.FormsAuthentication Class -- https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthentication?view=netframework-4.8

            //FormsAuthenticationTicket Class
            //https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthenticationticket?view=netframework-4.8


            //1.建立FormsAuthenticationTicket
            var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.Email.ToString(), //可以放使用者Id
            issueDate: DateTime.UtcNow,//現在UTC時間
            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
            isPersistent: loginVM.Remember,// 是否要記住我 true or false
            userData: user.FirstName.ToString(), //可以放使用者角色名稱
            cookiePath: FormsAuthentication.FormsCookiePath);

            //2.加密Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //3.Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            //4.取得original URL.
            var url = FormsAuthentication.GetRedirectUrl(name, true);

            //5.導向original URL
            return Redirect(url);
        }
    }
}