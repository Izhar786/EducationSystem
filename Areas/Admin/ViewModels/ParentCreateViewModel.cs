using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.ViewModels
{
    public class ParentCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Cnic { get; set; }

        [Required]
        public int GenderId { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
    }
}
