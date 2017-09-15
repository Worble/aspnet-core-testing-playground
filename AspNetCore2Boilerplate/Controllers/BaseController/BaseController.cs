using AspNetCore2Boilerplate.Models;
using AspNetCore2Boilerplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCore2Boilerplate.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationUser CurrentUser
        {
            get
            {
                return new ApplicationUser(this.User as ClaimsPrincipal);
            }
        }
    }
}
