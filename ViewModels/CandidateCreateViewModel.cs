using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.ViewModels
{
    public class CandidateCreateViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Display(Name = "Cnic")]
        public string Cnic { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "{0} is required")]
        public int GenderId { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }

        [Display(Name = "Parent")]
        [Required(ErrorMessage = "{0} is required")]
        public int ParentId { get; set; }

        [Display(Name = "Fee payer")]
        [Required(ErrorMessage = "{0} is required")]
        public int FeePayerId { get; set; }

    }
}
