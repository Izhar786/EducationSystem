using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            return ViewComponent("EducationSystem.Areas.Admin.ViewComponents.UserList");
        }
    }
}
