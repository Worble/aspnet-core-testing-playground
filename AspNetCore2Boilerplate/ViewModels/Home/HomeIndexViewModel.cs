using BoilerplateData.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore2Boilerplate.ViewModels
{
    public class HomeIndexViewModel
    {
        public int Page { get; set; }
        public int ResultAmount { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<SelectListItem> ResultAmounts { get; set; }
        public int TotalPages { get; set; }
    }
}
