using System.Linq;
using AspnetNote.Models;
using AspnetNote.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetNote.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Login(LoginViewModel model)
        {
            // ID, 비밀번호 - 필수
            if(ModelState.IsValid)
            {
                using (var db = new Data.AspnetNoteDbContext())
                {
                    // Linq - Method Chaining
                    // FirstOrDefault() : 조건을 만족하는 첫번째 데이터 출력
                    // => : A Got to B
                    // var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId && u.UserPassword == model.UserPassword);
                    var user = db.Users
                        .FirstOrDefault(u => u.UserId.Equals(model.UserId) &&
                                        u.UserPassword.Equals(model.UserPassword));

                    if (user != null)
                    {
                        // 로그인에 성공했을 때
                        // HttpContext.Session.SetInt32(key, value);
                        HttpContext.Session.SetInt32("User_Login_Key", user.UserNo);
                        return RedirectToAction("LoginSuccess", "Home");
                    }
                }

                // 로그인에 실패했을 때
                ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");
            }

            return View(model);
        }

        /// <summary>
        /// 로그아웃
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            // 모든 Session 삭제
            // HttpContext.Session.Clear();

            HttpContext.Session.Remove("User_Login_Key");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 회원가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 회원가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid)
            {
                // DB Connection Open
                using (var db = new Data.AspnetNoteDbContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges();   //Commit

                }
                // DB Connection Close

                return RedirectToAction("Index","Home");

            }
            return View();
        }
    }
}
