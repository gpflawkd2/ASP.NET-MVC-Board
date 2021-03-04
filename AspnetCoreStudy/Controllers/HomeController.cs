using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreStudy.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreStudy.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //var hongUser = new User();
            //hongUser.UserNo = 1;
            //hongUser.UserName = "홍길동";

            var hongUser = new User()
            {
                UserNo = 1,
                UserName = "홍길동"
            };

            // # 1번째 방식 View(model)
            //return View(hongUser);

            // # 2번째 방식 ViewBag
            //ViewBag.User = hongUser;
            //return View();

            // # 3번째 방식 ViewData -> 단일값만 전송 가능
            ViewData["UserNo"] = hongUser.UserNo;
            ViewData["UserName"] = hongUser.UserName;

            return View();

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}