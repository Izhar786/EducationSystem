using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClassesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
