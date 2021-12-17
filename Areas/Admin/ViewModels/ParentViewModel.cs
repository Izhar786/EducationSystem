using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.ViewModels
{
    public class ParentViewModel
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Cnic { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
    }
}
