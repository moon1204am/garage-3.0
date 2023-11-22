using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage.Data.Data;
using Garage.Domain.Entities;
using Garage.Web.Models.ViewModels;
using Garage3._0.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections;
using System.Drawing.Printing;

namespace Garage2._0.Controllers
{
    public class PersonController : Controller
    {
        private readonly Garage2_0Context _context;
        const int PageSize = 3;

        public PersonController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index(int page = 0)
        {
            
            var selection = await _context.Person.Select(v => new PersonOverViewViewModel

            {
                PersonId = v.PersonId,
                FirstName = v.FirstName,
                LastName = v.LastName,
                SSN = v.SSN,
                NumberOfParkedVehicles = _context.Vehicle.Where(p => p.PersonId == v.PersonId).Count(),
            }).OrderByDescending(p => p.FirstName.Substring(0, 2))
             .ToListAsync();

            selection.Reverse();         
            var count = selection.Count;
  
            var index = new PersonIndexViewModel
            {
                Members = selection.Skip(page * PageSize).Take(PageSize).ToList()
            };
                  
            ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(index);
        }



            
        

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            var personDetailsViewModel = new PersonDetailsViewModel
            {
                SSN = person.SSN,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonId = person.PersonId,
                Vehicles = await _context.Vehicle.Where(p => p.PersonId == person.PersonId).Include(v => v.VehicleType).ToListAsync()
            };

            return View(personDetailsViewModel);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,SSN,FirstName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,SSN,FirstName,LastName")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'Garage2_0Context.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
          return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Filter(PersonIndexViewModel personIndexViewModel, int page = 0)
        {
            
            var query = string.IsNullOrWhiteSpace(personIndexViewModel.LastName) ?
                                               _context.Person :
                                               _context.Person.Where(p => p.LastName.StartsWith(personIndexViewModel.LastName));

            var tempData = await query.Select(v => new PersonOverViewViewModel
            {
                PersonId = v.PersonId,
                FirstName = v.FirstName,
                LastName = v.LastName,
                SSN = v.SSN,
                NumberOfParkedVehicles = _context.Vehicle.Where(p => p.PersonId == v.PersonId).Count(),
            }).OrderByDescending(p => p.FirstName.Substring(0, 2))
             .ToListAsync();

            tempData.Reverse();
            var count = tempData.Count;

            var querySelect = new PersonIndexViewModel
            {
                Members = tempData.Skip(page * PageSize).Take(PageSize).ToList()
            };
  
            ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(nameof(Index), querySelect);
        }
    }
}
