

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ActionResult = System.Web.Mvc.ActionResult;

namespace cart.Controllers
{
    [Authorize]
    public class RegisterController : Controller
    {
        // GET: Register
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Models.City> city = new List<Models.City>();
            using (Models.CityVilliageEntities _db = new Models.CityVilliageEntities())
            {
                ViewBag.ResultMessage = TempData["ResultMessage"];
                city = (from c in _db.Cities select c).ToList();
                ViewBag.cityList = city;

            }
            return View(new Models.User());
        }
        [System.Web.Mvc.HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Models.User user)
        {
            string cityName = "";
            using (Models.CityVilliageEntities _db = new Models.CityVilliageEntities())
            {
                List<Models.City> city = new List<Models.City>();
                city = (from c in _db.Cities select c).ToList();
                ViewBag.cityList = city;
                cityName = (from c in _db.Cities where c.Id == user.city select c.Name).First();
            }

            if (user.password1.Trim() != user.password2.Trim())
            {
                ViewBag.Msg = "密碼輸入有誤";
                return View(user);
            }
            else
            {
                using (Models.UserInfoEntities1 _db = new Models.UserInfoEntities1())
                {
                    Models.userinfo userinfo = new Models.userinfo();
                    userinfo.account = user.account;
                    userinfo.password1 = user.password1;
                    userinfo.city = cityName;
                    userinfo.villiage = user.villiage;
                    _db.userinfoes.Add(userinfo);
                    _db.SaveChanges();

                }

                //不回重新發請求
                Response.Redirect("Login");
                return new System.Web.Mvc.EmptyResult();
            }

            //TempData["ResultMessage"] = account + city + villiage;

            
            //return RedirectToAction("Login");
        }
        
        [System.Web.Mvc.HttpPost]
        public ActionResult Villiage(Models.Villiage vi)
        {
            List<String> villiages = new List<String>();
            using (Models.CityVilliageEntities _db = new Models.CityVilliageEntities())
            {
                villiages = (from v in _db.Villiages where v.CityId == vi.CityId select v.CityVilliage).ToList();
                TempData["ResultMessage"] = "show--" + vi.CityId + "--show";
            }
          
            //name 為null 沒有傳值過來 會傳回JSON格式的物件
            return Json(new {success = true,villiage = villiages});
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            return View();
        }

        [AllowAnonymous]
        [System.Web.Mvc.HttpPost]
        public ActionResult Login(Models.userinfo user)
        {
            if (ModelState.IsValid)
            {
                using(Models.UserInfoEntities1 _db = new Models.UserInfoEntities1())
                {
                    Models.userinfo u = (from i in _db.userinfoes where i.account == user.account select i).FirstOrDefault();
                    if (u != null)
                    {
                        if (u.account == user.account && u.password1 == user.password1)
                        {
                            ViewBag.ResultMessage = "登入成功";
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                1,
                                user.account,
                                DateTime.Now,
                                DateTime.Now.AddMinutes(60),
                                //是否將 Cookie 設定成 Session Cookie，如果是則會在瀏覽器關閉後移除
                                true,
                                //要與票證一同存放的使用者特定資料
                                "",
                                //儲存 Cookie 的路徑
                                FormsAuthentication.FormsCookiePath
                                );
                            //加密ticket，結果儲存在 cookie 中 FormsCookieName
                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                            HttpCookie authCookie = new HttpCookie(
                                FormsAuthentication.FormsCookieName,
                                encryptedTicket);
                            // 使用者瀏覽器加入完成驗證的 Cookie
                            Response.Cookies.Add(authCookie);
                            Session["login"] = true;
                            Session["LoginUser"] = user.account;
                        }
                        else
                        {
                            ViewBag.ResultMessage = "輸入有誤";
                        }
                    }
                    else
                    {
                        ViewBag.ResultMessage = "無此帳號，請先註冊";
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("login");
            return RedirectToAction("Login");
        }
        
}
}
//https://ithelp.ithome.com.tw/articles/10159775
//https://ithelp.ithome.com.tw/articles/10196467