using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Controllers
{
    public class HelperController : Controller
    {
        [HttpGet]
        public IActionResult SearchParent()
        {

            return PartialView();
        }
    }
}
