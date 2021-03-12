using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetNote.Data;
using AspnetNote.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetNote.Controllers
{
    public class NoteController : Controller
    {
        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("User_Login_Key") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            //var list = new List<Note>();

            using (var db = new AspnetNoteDbContext())
            {
                var list = db.Notes.ToList();
                return View(list);
            }
        }

        /// <summary>
        /// 게시판 상세
        /// </summary>
        /// <param name="noteNo"></param>
        /// <returns></returns>
        public IActionResult Detail(int noteNo)
        {
            if (HttpContext.Session.GetInt32("User_Login_Key") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }

        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("User_Login_Key") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        /// <summary>
        /// 게시물 추가 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetInt32("User_Login_Key") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            //int.Parse : integer값으로 변환
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("User_Login_Key").ToString());

            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    db.Notes.Add(model);

                    //SaveChanges(): 성공한 Row수를 반환해줌
                    //var result = db.SaveChanges();    //Commit

                    if (db.SaveChanges() > 0)  
                    {
                        return Redirect("Index");
                    }
                    ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int noteNo)
        {
            if (HttpContext.Session.GetInt32("User_Login_Key") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }
        }

        /// <summary>
        /// 게시물 수정 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Note model)
        {
            if (HttpContext.Session.GetInt32("User_Login_Key") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            //int.Parse : integer값으로 변환
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("User_Login_Key").ToString());

            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    db.Notes.Update(model);

                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                    ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int noteNo)
        {
            if (HttpContext.Session.GetInt32("User_Login_Key") == null)
            {
                //로그인이 안된 상태
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));

                db.Notes.Remove(note);

                if (db.SaveChanges() > 0)
                {
                    return Redirect("Index");
                }
            }
            return Redirect("Index");
        }

    }
}
