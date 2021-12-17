using EducationSystem.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Areas.Admin.ViewComponents
{
    public class UserListViewComponent : ViewComponent
    {
        public UserListViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private Task<List<UserViewModel>> GetItemsAsync()
        {
            List<UserViewModel> users = new List<UserViewModel>()
            {
                new UserViewModel
                {
                    Name = "Izharullah",
                    Cnic = "3520215688997",
                    DateOfBirth = DateTime.Now.AddYears(-26),
                    Email = "izhar@askhorizons.com"
                },
                new UserViewModel
                {
                    Name = "Asadullah",
                    Cnic = "3520268956552",
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    Email = "asad@askhorizons.com"
                }
            };
            return Task.FromResult(users);
        }
    }
}
