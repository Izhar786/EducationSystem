using EducationSystem.Areas.Admin.ViewModels;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParentsController : Controller
    {
        private readonly DataContext _context;

        public ParentsController(DataContext context)
        {
            _context = context;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Load Data
        [HttpGet]
        public IActionResult GetData(string cnic, string name)
        {
            return ViewComponent("EducationSystem.Areas.Admin.ViewComponents.ParentList", 
                new { cnic, name });
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genders = await _context.Genders.ToListAsync();
            var model = new ParentCreateViewModel
            {
                Genders = genders.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.GenderId.ToString()
                })
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var record = new Parent
                {
                    Cnic = model.Cnic,
                    GenderId = model.GenderId,
                    Name = model.Name
                };

                await _context.Parents.AddAsync(record);
                if (!(await _context.SaveChangesAsync() > 0))
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            var genders = await _context.Genders.ToListAsync();
            model.Genders = genders.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.GenderId.ToString()
            });

            return PartialView(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ParentId == id);
            if (parent == null)
            {
                return NotFound();
            }

            var genders = await _context.Genders.ToListAsync();
            var model = new ParentUpdateViewModel
            {
                Genders = genders.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.GenderId.ToString()
                }),
                Cnic = parent.Cnic,
                GenderId = parent.GenderId,
                ParentId = parent.ParentId,
                Name = parent.Name
            };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ParentUpdateViewModel model)
        {

            if (ModelState.IsValid)
            {
                var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ParentId == model.ParentId);
                if (parent == null)
                {
                    return NotFound();
                }

                parent.Cnic = model.Cnic;
                parent.GenderId = model.GenderId;
                parent.Name = model.Name;

                _context.Parents.Update(parent);

                if (!(await _context.SaveChangesAsync() > 0))
                {
                    ModelState.AddModelError("", "Error while saving data");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required data to proceed");
            }

            var genders = await _context.Genders.ToListAsync();
            model.Genders = genders.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.GenderId.ToString()
            });

            return PartialView(model);
        }
        #endregion

        #region Toggle
        [HttpPost]
        public async Task<IActionResult> Toggle(int id)
        {
            bool status = false;
            string message = string.Empty;

            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.ParentId == id);
            if (parent == null)
            {
                return NotFound();
            }

            parent.IsActive = parent.IsActive ? false : true;
            _context.Parents.Update(parent);

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
    }
}