using BoilerplateData.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore2Boilerplate.ViewModels
{
    public class HomeIndexViewModel
    {
        public int Page { get; set; }
        public int ResultAmount { get; set; }
        public List<UserDTO> Users { get; set; }

        [Display(Name = "Number of results per page:")]
        public List<int> ResultAmounts { get; set; }
        public int TotalPages { get; set; }

        [Display(Name = "Search for:")]
        public string Search { get; set; }
    }
}
