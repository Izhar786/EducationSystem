using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.ViewModels
{
    public class SessionViewModel
    {
        public int SessionId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
