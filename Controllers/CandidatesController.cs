using AutoMapper;
using EducationSystem.Models;
using EducationSystem.ViewModels;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<CandidatesController> _logger;
        private readonly IMapper _mapper;

        public CandidatesController(
            DataContext context,
            ILogger<CandidatesController> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Create New
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CandidateCreateViewModel
            {
                Genders = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Male", Value = "1" },
                    new SelectListItem { Text = "Female", Value = "2" },
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var candidate = new Candidate
                {
                    Cnic = model.Cnic,
                    DateOfBirth = model.DateOfBirth,
                    FeePayerId = model.FeePayerId,
                    GenderId = model.GenderId,
                    Name = model.Name,
                    ParentId = model.ParentId
                };

                await _context.Candidates.AddAsync(candidate);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return RedirectToAction("Index", new { area = "", controller = "Candidates" });
                }
            }

            model.Genders = new List<SelectListItem>
            {
                new SelectListItem { Text = "Male", Value = "1" },
                new SelectListItem { Text = "Female", Value = "2" },
            };

            return View(model);
        }
        #endregion

        #region Load Data
        [HttpPost]
        public IActionResult GetData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                var name = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var email = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var cnic = Request.Form["columns[3][search][value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;


                var candidates = new List<Candidate>
                {
                    new Candidate { CandidateId = 1, Email = "user1@domain.com", Cnic = "6110117675441", DateOfBirth = DateTime.Now.AddYears(-15), GenderId = 1, Name = "Salman Malik" },
                    new Candidate { CandidateId = 2, Email = "user2@domain.com", Cnic = "6110117675445", DateOfBirth = DateTime.Now.AddYears(-10), GenderId = 1, Name = "Ali Taj" },
                    new Candidate { CandidateId = 3, Email = "user3@domain.com", Cnic = "6110117675447", DateOfBirth = DateTime.Now.AddYears(-9), GenderId = 1, Name = "Izharullah" },
                    new Candidate { CandidateId = 4, Email = "user4@domain.com", Cnic = "6110117675449", DateOfBirth = DateTime.Now.AddYears(-8), GenderId = 1, Name = "Asadullah" },
                };

                var model = _mapper.Map<List<CandidateModel>>(candidates);

                if (!string.IsNullOrEmpty(name))
                {
                    model = model.Where(m => m.Name.ToUpper().Contains(name.ToUpper())).ToList();
                }

                if (!string.IsNullOrEmpty(email))
                {
                    model = model.Where(m => m.Email.ToUpper().Contains(email.ToUpper())).ToList();
                }

                if (!string.IsNullOrEmpty(cnic))
                {
                    model = model.Where(m => m.Cnic.ToUpper().Contains(cnic.ToUpper())).ToList();
                }

                //Generic search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    model = model.Where(m => m.Name.Contains(searchValue.ToUpper()) ||
                                                   m.Email.Contains(searchValue.ToUpper()) ||
                                                   m.Cnic.Contains(searchValue.ToUpper())).ToList();
                }

                //total number of rows count   
                recordsTotal = model.Count();

                //Paging   
                var data = model
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();

                return Ok(
                    new
                    {
                        draw = draw,
                        recordsTotal = recordsTotal,
                        recordsFiltered = data.Count,
                        data = data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: {ex.Message}");
                return BadRequest();
            }
        }
        #endregion
    }
}
