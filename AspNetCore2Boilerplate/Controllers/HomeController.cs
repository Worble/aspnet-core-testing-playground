using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCore2Boilerplate.ViewModels;
using BoilerplateData.Work.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AspNetCore2Boilerplate.Controllers
{
    public class HomeController : BaseController
    {
        private IUnitOfWork work;
        private IHostingEnvironment hostingEnvironment;

        public HomeController(IUnitOfWork work, IHostingEnvironment hostingEnvironment)
        {
            this.work = work;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 0, int resultAmount = 15, string search = "")
        {
            if(pageNumber < 0)
            {
                pageNumber = 0;
            }
            if(resultAmount < 1)
            {
                resultAmount = 1;
            }
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }
            var Model = new HomeIndexViewModel()
            {
                Page = pageNumber,
                ResultAmount = resultAmount,
                ResultAmounts = new List<int>() { 5, 10, 15, 30 },
                Users = work.UserRepository.GetPage(pageNumber, resultAmount, search),
                TotalPages = work.UserRepository.GetTotalPages(resultAmount, search)
            };
            return View(Model);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> About(IFormCollection form)
        {
            if(Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    };
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpGet("Error/")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Forbidden/")]
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
