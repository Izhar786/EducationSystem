using EducationSystem.Areas.Admin.ViewModels;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SessionsController : Controller
    {
        private readonly DataContext _context;

        public SessionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new Session
                {
                    Name = model.Name
                };

               await _context.Sessions.AddAsync(data);
                if (!(await _context.SaveChangesAsync() > 0))
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide required field");
            }
            return PartialView(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(p => p.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }

            var model = new SessionUpdateViewModel
            {
                Name = session.Name,
                SessionId = session.SessionId
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SessionUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var session = await _context.Sessions.FirstOrDefaultAsync(p => p.SessionId == model.SessionId);
                if (session == null)
                {
                    return NotFound();
                }

                session.Name = model.Name;

               _context.Sessions.Update(session);
                if (!(_context.SaveChanges()>0))
                {
                    ModelState.AddModelError("", "Error while updating data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide required field");
            }

            return PartialView(model);
        }
        #endregion

        #region Toggle
        [HttpPost]
        public async Task<IActionResult> Toggle(int id)
        {
            bool status = false;
            string message = string.Empty;

            var session = await _context.Sessions.FirstOrDefaultAsync(p => p.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }

            session.IsActive = session.IsActive ? false : true;
            _context.Sessions.Update(session);

            if ((await _context.SaveChangesAsync() > 0))
            {
                status = true;
                message = "Record updated successfully";
            }
            else
            {
                ModelState.AddModelError("", "Error while saving data");
                message = "Error saving changes";
            }

            return Json(new { status, message });
        }
        #endregion


        #region LoadData

        [HttpGet]
        public IActionResult LoadData()
        {
            return ViewComponent("EducationSystem.Areas.Admin.ViewComponents.SessionList");
        }

        #endregion
    }
}
