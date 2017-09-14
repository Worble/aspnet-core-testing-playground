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

namespace AspNetCore2Boilerplate.Controllers
{
    public class HomeController : BaseController
    {
        private IUnitOfWork work;

        public HomeController(IUnitOfWork work)
        {
            this.work = work;
        }

        public IActionResult Index(int pageNumber = 0, int resultAmount = 15)
        {
            if(pageNumber < 0)
            {
                pageNumber = 0;
            }

            if(resultAmount < 1)
            {
                resultAmount = 1;
            }

            var Model = new HomeIndexViewModel()
            {
                Page = pageNumber,
                ResultAmount = resultAmount,
                Users = work.UserRepository.GetPage(pageNumber, resultAmount),
                TotalPages = work.UserRepository.GetTotalPages(resultAmount)
            };

            return View(Model);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
