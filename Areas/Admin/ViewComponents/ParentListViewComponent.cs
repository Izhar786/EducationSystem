using EducationSystem.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.ViewComponents
{
    public class ParentListViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public ParentListViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string cnic, string name)
        {
            var items = await GetItemsAsync(cnic, name);
            return View(items);
        }
        private async Task<IEnumerable<ParentViewModel>> GetItemsAsync(string cnic, string name)
        {
            var temp = await _context.Parents
                .Include(p => p.Gender)
                .ToListAsync();

            if (!string.IsNullOrEmpty(cnic) && !string.IsNullOrWhiteSpace(cnic))
            {
                temp = temp.Where(p => p.Cnic.Contains(cnic)).ToList();
            }

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                temp = temp.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
            }

            return
                temp.Select(p => new ParentViewModel
                {
                    ParentId = p.ParentId,
                    Cnic = p.Cnic,
                    Gender = p.Gender.Name,
                    GenderId = p.GenderId,
                    Name = p.Name,
                    IsActive = p.IsActive
                });
        }
    }
}
