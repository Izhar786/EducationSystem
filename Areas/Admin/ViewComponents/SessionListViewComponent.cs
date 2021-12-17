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
    public class SessionListViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public SessionListViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private async Task<IEnumerable<SessionViewModel>> GetItemsAsync()
        {
            var temp = await _context.Sessions
                .ToListAsync();

            return
                temp.Select(p => new SessionViewModel
                {
                    SessionId = p.SessionId,
                    Name = p.Name,
                    IsActive = p.IsActive
                });
        }
    }
}
