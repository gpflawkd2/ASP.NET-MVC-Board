using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetNote.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;

        //ctor + tab + tab: 생성자 생성
        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        // http://www.example.com/Upload/ImageUpload = http://www.example.com/api/upload
        // Route(): Url 재정의
        [HttpPost, Route("api/upload")]
        //[Route("")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // # 이미지나 파일을 업로드 할 때 필요한 구성

            // 1. Path(경로) - 저장경로 지정
            //  - Path.Combine("1", "/2", "/3", "/4", ...) -> 결과 1/2/3/4 : String을 계속 이어줌
            var path = Path.Combine(_environment.WebRootPath, @"images\upload");    // @: stirng으로 인식함

            // 2. Name(이름) - DateTime, GUID + GUID
            // 3. Extension(확장자) - jpg, png, txt...

            //var fileName = file.FileName;   //원본파일명

            var fileFullName = file.FileName.Split('.');
            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";

            var filePath = Path.Combine(path, fileName);    //경로

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok(new { file="/images/upload/" + fileName, success = true });

            // # URL 접근 방식
            // ASP.NET - 호스트명/ + api/upload => http://www.example.com/api/upload
            // JavaScript - 호스트명 + / + api/upload => http://www.example.com/api/upload
        }
    }
}
